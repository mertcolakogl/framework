// Haptics.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Runtime.InteropServices;
using Gambit.Framework.Scripts.Core.Haptic.Enum;
using UnityEngine;
#if UNITY_ANDROID
using Gambit.Framework.Scripts.Core.Haptic.Behaviour;
#endif

namespace Gambit.Framework.Scripts.Other.Utils
{
    public static class Haptics
    {
        public static void Feedback(HapticTypes type)
        {
            if (Application.isEditor)
            {
                Debug.Log("Haptic Feedback: " + type);
                return;
            }

#if UNITY_IOS
            if (HasHapticEngine()) _Feedback(type.ToString());
            else Handheld.Vibrate();
#elif UNITY_ANDROID
            AndroidHaptic.Feedback(type.ToString());
#endif
        }

        private static bool HasHapticEngine()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return !SystemInfo.deviceModel.StartsWith("iPad") && !SystemInfo.deviceModel.StartsWith("iPod");
#else
            return false;
#endif
        }

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _Feedback(string type);
#endif
    }
}