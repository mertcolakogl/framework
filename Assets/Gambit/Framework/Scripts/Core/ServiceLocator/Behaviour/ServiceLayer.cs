// ServiceLayer.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Audio.Interface;
using Gambit.Framework.Scripts.Core.Haptic.Enum;
using Gambit.Framework.Scripts.Core.Haptic.Interface;
using Gambit.Framework.Scripts.Core.Log.Interface;
using Gambit.Framework.Scripts.Core.Pooling.Interface;
using Gambit.Framework.Scripts.Core.Prefs.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;
using Gambit.Framework.Scripts.Core.StateMachine.Interface;
using Gambit.Framework.Scripts.Core.Tween.Interface;
using Gambit.Framework.Scripts.Core.View;
using Gambit.Framework.Scripts.Other.Extensions;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.ServiceLocator.Behaviour
{
    public class ServiceLayer : IServiceLayer
    {
        private IAudioService _audio;
        private IHapticService _haptic;
        private ILoggerService _logger;
        private IPoolService _pool;
        private IPlayerPrefService _prefs;
        private IStateService _state;
        private ITweenService _tween;

        private IAudioService Audio
        {
            get { return _audio ??= Of<IAudioService>(); }
        }

        private IHapticService Haptic
        {
            get { return _haptic ??= Of<IHapticService>(); }
        }

        private ILoggerService Logger
        {
            get { return _logger ??= Of<ILoggerService>(); }
        }

        private IPlayerPrefService Prefs
        {
            get { return _prefs ??= Of<IPlayerPrefService>(); }
        }

        private IPoolService Pool
        {
            get { return _pool ??= Of<IPoolService>(); }
        }

        private IStateService State
        {
            get { return _state ??= Of<IStateService>(); }
        }

        private ITweenService Tween
        {
            get { return _tween ??= Of<ITweenService>(); }
        }

        #region Interface implementation

        public T Of<T>() where T : IService
        {
            return ServiceLocator.Instance.Of<T>();
        }

        #endregion

        #region IAudioService implementation

        protected void SetAudio(bool value)
        {
            if (Audio.IsActive == value) return;
            Audio.SetAudio(value);
            Prefs.SetPref("audio", Audio.IsActive);
        }

        protected void PlayOneShot(AudioClip clip)
        {
            if (Audio.IsActive) Audio.PlayOneShot(clip);
        }

        #endregion

        #region IHapticService implementation

        protected void SetHaptic(bool value)
        {
            if (Haptic.IsActive == value) return;
            Haptic.SetHaptic(value);
            Prefs.SetPref("haptic", Haptic.IsActive);
        }

        protected void HapticFeedback(HapticTypes type)
        {
            if (Haptic.IsActive) Haptic.HapticFeedback(type);
        }

        #endregion

        #region ILoggerService implementation

        protected void DumpHistories()
        {
            Logger.DumpHistories();
        }

        protected void LogTrace(string msg, GameObject obj = null)
        {
            Logger.LogTrace(msg, obj);
        }

        protected void Log(string msg, GameObject obj = null)
        {
            Logger.Log(msg, obj);
        }

        protected void LogWarning(string msg, GameObject obj = null)
        {
            Logger.LogWarning(msg, obj);
        }

        protected void LogError(string msg, GameObject obj = null)
        {
            Logger.LogError(msg, obj);
        }

        #endregion

        #region IPoolService implementation

        protected GameObject Dequeue(GameObject prefab, Vector3 pos = default)
        {
            var key = prefab.Get<PoolMediator>().Key;
            var obj = Pool.Dequeue(key, prefab);
            obj.transform.position = pos;
            return obj;
        }

        protected void Enqueue(GameObject prefab)
        {
            var key = prefab.Get<PoolMediator>().Key;
            Pool.Enqueue(key, prefab);
        }

        #endregion

        #region IPlayerPrefService implementation

        protected T GetPref<T>(string key, T value = default)
        {
            return Prefs.GetPref(key, value);
        }

        protected void SetPref<T>(string key, T value)
        {
            Prefs.SetPref(key, value);
        }

        #endregion

        #region IStateService implementation

        protected IState GetState(string key, string state)
        {
            return State.GetState(key, state);
        }

        protected IState GetState(string key, int index)
        {
            return State.GetState(key, index);
        }

        protected void SetState(string key, string state)
        {
            State.SetState(key, state);
        }

        protected void SetState(string key, int index)
        {
            State.SetState(key, index);
        }

        #endregion

        #region ITweenService implementation

        protected void AddTween(ITween tween)
        {
            Tween.AddTween(tween);
        }

        protected void RemoveTween(ITween tween)
        {
            Tween.RemoveTween(tween);
        }

        #endregion
    }
}