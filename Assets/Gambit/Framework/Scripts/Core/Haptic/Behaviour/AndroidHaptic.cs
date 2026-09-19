// AndroidHaptic.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Haptic.Behaviour
{
    public static class AndroidHaptic
    {
        private const int SdkAmplitude = 26; // VibrationEffect.createOneShot/createWaveform
        private const int SdkPredefined = 29; // VibrationEffect.createPredefined (EFFECT_*)
        private const int SdkComposition = 30; // VibrationEffect.Composition (PRIMITIVE_*)
        private const long Selection = 8;
        private const long Light = 10;
        private const long Medium = 15;
        private const long Heavy = 40;
        private const int SelectionAmpl = 48;
        private const int LightAmpl = 60;
        private const int MediumAmpl = 135;
        private const int HeavyAmpl = 240;
        private static readonly long[] Warning = { 0, Medium, Heavy, Light };
        private static readonly int[] WarningAmpl = { 0, HeavyAmpl, 0, MediumAmpl };
        private static readonly long[] Failure = { 0, Light, Heavy, Light, Heavy, Light };
        private static readonly int[] FailureAmpl = { 0, MediumAmpl, 0, MediumAmpl, 0, MediumAmpl };
        private static readonly long[] Success = { 0, Light, Medium, Heavy };
        private static readonly int[] SuccessAmpl = { 0, LightAmpl, 0, HeavyAmpl };

        // Cached JNI handles
        private static int _sdkVersion = -1;
        private static AndroidJavaObject _androidVibrator;
        private static AndroidJavaObject _currentActivity;
        private static AndroidJavaClass _compositionClass;
        private static AndroidJavaClass _vibrationEffectClass;

        // Predefined effect id cache (SDK 29+)
        private static int _effectClick = -1;
        private static int _effectTick = -1;
        private static int _effectHeavyClick = -1;

        // Primitive id cache (SDK 30+)
        private static int _primitiveClick = -1;
        private static int _primitiveTick = -1;
        private static int _primitiveQuickRise = -1;
        private static bool _vibratorInitialized;

        public static void Feedback(string key)
        {
            try
            {
                Action action = key switch
                {
                    "Warning" => () => CreateWaveform(Warning, WarningAmpl, BuildWarningComposition),
                    "Failure" => () => CreateWaveform(Failure, FailureAmpl, BuildFailureComposition),
                    "Success" => () => CreateWaveform(Success, SuccessAmpl, BuildSuccessComposition),
                    "Light" => () => CreateOneShot(Light, LightAmpl, GetEffectTick),
                    "Medium" => () => CreateOneShot(Medium, MediumAmpl, GetEffectClick),
                    "Heavy" => () => CreateOneShot(Heavy, HeavyAmpl, GetEffectHeavyClick),
                    "Selection" => () => CreateOneShot(Selection, SelectionAmpl, GetEffectTick),
                    _ => () => Debug.Log("AndroidHaptic: unknown type - " + key)
                };
                action();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private static void CreateOneShot(long ms, int amplitude, Func<int> predefinedEffectGetter)
        {
            if (!EnsureVibratorInitialized()) return;
            var sdk = AndroidSDKVersion();
            if (sdk >= SdkPredefined)
            {
                var effectId = predefinedEffectGetter();
                if (effectId != -1)
                {
                    EnsureVibrationEffectClass();
                    var effect = _vibrationEffectClass.CallStatic<AndroidJavaObject>("createPredefined", effectId);
                    if (effect != null)
                    {
                        _androidVibrator.Call("vibrate", effect);
                        return;
                    }
                }
            }

            if (sdk >= SdkAmplitude)
            {
                EnsureVibrationEffectClass();
                var effect = _vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", ms, amplitude);
                if (effect != null)
                {
                    _androidVibrator.Call("vibrate", effect);
                    return;
                }
            }

            _androidVibrator.Call("vibrate", ms);
        }

        private static void CreateWaveform(long[] pattern, int[] amplitudes, Func<AndroidJavaObject> compositionBuilder)
        {
            if (!EnsureVibratorInitialized()) return;
            var sdk = AndroidSDKVersion();
            if (sdk >= SdkComposition)
            {
                var composed = compositionBuilder();
                if (composed != null)
                {
                    _androidVibrator.Call("vibrate", composed);
                    return;
                }
            }

            if (sdk >= SdkAmplitude)
            {
                EnsureVibrationEffectClass();
                var effect = _vibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", pattern, amplitudes, -1);
                if (effect != null)
                {
                    _androidVibrator.Call("vibrate", effect);
                    return;
                }
            }

            _androidVibrator.Call("vibrate", pattern, -1);
        }

        private static AndroidJavaObject BuildWarningComposition()
        {
            var tick = GetPrimitiveTick();
            var click = GetPrimitiveClick();
            if (tick == -1 || click == -1) return null;
            EnsureVibrationEffectClass();
            using var composition = _vibrationEffectClass.CallStatic<AndroidJavaObject>("startComposition");
            composition.Call<AndroidJavaObject>("addPrimitive", click, 1.0f)?.Dispose();
            composition.Call<AndroidJavaObject>("addPrimitive", tick, 0.6f)?.Dispose();
            return composition.Call<AndroidJavaObject>("compose");
        }

        private static AndroidJavaObject BuildFailureComposition()
        {
            var tick = GetPrimitiveTick();
            var click = GetPrimitiveClick();
            if (tick == -1 || click == -1) return null;
            EnsureVibrationEffectClass();
            using var composition = _vibrationEffectClass.CallStatic<AndroidJavaObject>("startComposition");
            composition.Call<AndroidJavaObject>("addPrimitive", click, 0.6f)?.Dispose();
            composition.Call<AndroidJavaObject>("addPrimitive", tick, 0.4f)?.Dispose();
            composition.Call<AndroidJavaObject>("addPrimitive", click, 0.6f)?.Dispose();
            return composition.Call<AndroidJavaObject>("compose");
        }

        private static AndroidJavaObject BuildSuccessComposition()
        {
            var quickRise = GetPrimitiveQuickRise();
            var click = GetPrimitiveClick();
            if (quickRise == -1 || click == -1) return null;
            EnsureVibrationEffectClass();
            using var composition = _vibrationEffectClass.CallStatic<AndroidJavaObject>("startComposition");
            composition.Call<AndroidJavaObject>("addPrimitive", quickRise, 1.0f)?.Dispose();
            composition.Call<AndroidJavaObject>("addPrimitive", click, 1.0f)?.Dispose();
            return composition.Call<AndroidJavaObject>("compose");
        }

        private static int GetEffectClick()
        {
            if (_effectClick != -1) return _effectClick;
            EnsureVibrationEffectClass();
            return _effectClick = _vibrationEffectClass.GetStatic<int>("EFFECT_CLICK");
        }

        private static int GetEffectTick()
        {
            if (_effectTick != -1) return _effectTick;
            EnsureVibrationEffectClass();
            return _effectTick = _vibrationEffectClass.GetStatic<int>("EFFECT_TICK");
        }

        private static int GetEffectHeavyClick()
        {
            if (_effectHeavyClick != -1) return _effectHeavyClick;
            EnsureVibrationEffectClass();
            return _effectHeavyClick = _vibrationEffectClass.GetStatic<int>("EFFECT_HEAVY_CLICK");
        }

        private static int GetPrimitiveClick()
        {
            if (_primitiveClick != -1) return _primitiveClick;
            EnsureCompositionClass();
            return _primitiveClick = _compositionClass.GetStatic<int>("PRIMITIVE_CLICK");
        }

        private static int GetPrimitiveTick()
        {
            if (_primitiveTick != -1) return _primitiveTick;
            EnsureCompositionClass();
            return _primitiveTick = _compositionClass.GetStatic<int>("PRIMITIVE_TICK");
        }

        private static int GetPrimitiveQuickRise()
        {
            if (_primitiveQuickRise != -1) return _primitiveQuickRise;
            EnsureCompositionClass();
            return _primitiveQuickRise = _compositionClass.GetStatic<int>("PRIMITIVE_QUICK_RISE");
        }

        private static bool EnsureVibratorInitialized()
        {
            if (_vibratorInitialized) return _androidVibrator != null;
            _vibratorInitialized = true;

            try
            {
                using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                _currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                _androidVibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return _androidVibrator != null;
        }

        private static void EnsureVibrationEffectClass()
        {
            _vibrationEffectClass ??= new AndroidJavaClass("android.os.VibrationEffect");
        }

        private static void EnsureCompositionClass()
        {
            _compositionClass ??= new AndroidJavaClass("android.os.VibrationEffect$Composition");
        }

        private static int AndroidSDKVersion()
        {
            if (_sdkVersion != -1) return _sdkVersion;
            try
            {
                var os = SystemInfo.operatingSystem;
                var id = os.IndexOf("-", StringComparison.Ordinal);
                if (id < 0 || id + 4 > os.Length)
                {
                    _sdkVersion = 0;
                    return _sdkVersion;
                }

                _sdkVersion = int.Parse(os.Substring(id + 1, 3));
            }
            catch (Exception e)
            {
                _sdkVersion = 0;
                Debug.LogException(e);
            }

            return _sdkVersion;
        }
    }
}