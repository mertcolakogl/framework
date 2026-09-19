// TweenBase.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Tween.Enum;
using Gambit.Framework.Scripts.Core.Tween.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Tween.Behaviour
{
    public class TweenBase : ITween
    {
        private float _elapsedDelay;
        private float _elapsedTime;
        private bool _isYoyoReversed;
        public float Delay;
        public float Duration;
        public Func<float, float> EaseFunc = Ease.Linear;
        public LoopTypes LoopType = LoopTypes.Restart;
        public Action<float, float> OnTweenAction;
        public Action<float, float> OnUpdate;

        public static TweenBuilder Create()
        {
            return new TweenBuilder { Tween = new TweenBase() };
        }

        private void UpdateWithEase(float duration)
        {
            var t = LoopType switch
            {
                LoopTypes.Yoyo when _isYoyoReversed => 1f - duration,
                LoopTypes.Incremental when ElapsedLoop > 0 => duration + ElapsedLoop,
                _ => duration
            };
            var ease = EaseFunc?.Invoke(t) ?? t;
            OnTweenAction?.Invoke(t, ease);
            OnUpdate?.Invoke(t, ease);
        }

        public void ImmediatelyComplete(bool onComplete = true)
        {
            if (IsKilled || !IsPlaying) return;
            UpdateWithEase(1f);
            ElapsedLoop++;
            if (onComplete) OnLoopComplete?.Invoke();
            IsPlaying = false;
            IsCompleted = true;
            if (onComplete) OnComplete?.Invoke();
        }

        #region Interface implementation

        public bool IsPlaying { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsKilled { get; set; }
        public int Loop { get; set; }
        public int ElapsedLoop { get; set; }
        public Action OnStart { get; set; }
        public Action OnComplete { get; set; }
        public Action OnLoopStart { get; set; }
        public Action OnLoopComplete { get; set; }

        public void Update(float deltaTime, Action<ITween> onUpdate = null)
        {
            if (IsKilled || !IsPlaying) return;
            _elapsedDelay += deltaTime;
            if (Delay > _elapsedDelay) return;
            if (!(_elapsedDelay > 0))
            {
                if (ElapsedLoop == 0) OnStart?.Invoke();
                OnLoopStart?.Invoke();
                if (Duration != 0f) UpdateWithEase(0);
            }

            _elapsedTime += deltaTime;
            if (Duration != 0f)
            {
                var t = Mathf.Clamp01(_elapsedTime / Duration);
                UpdateWithEase(t);
            }
            else
            {
                UpdateWithEase(1f);
            }

            if (_elapsedTime >= Duration)
            {
                ElapsedLoop++;
                _elapsedDelay = 0;
                _elapsedTime = 0;
                OnLoopComplete?.Invoke();
                if (LoopType == LoopTypes.Yoyo) _isYoyoReversed = !_isYoyoReversed;
            }

            var shouldComplete = Loop switch { -1 => false, 0 => ElapsedLoop >= 1, _ => ElapsedLoop >= Loop };
            if (!shouldComplete) return;
            IsPlaying = false;
            IsCompleted = true;
            OnComplete?.Invoke();
        }

        public void Play(Action<ITween> onPlay = null)
        {
            IsPlaying = true;
            onPlay?.Invoke(this);
        }

        public void Kill(Action<ITween> onKill = null)
        {
            IsKilled = true;
            IsPlaying = false;
            onKill?.Invoke(this);
        }

        public void Reset()
        {
            _elapsedDelay = 0f;
            _elapsedTime = 0f;
            _isYoyoReversed = false;
            ElapsedLoop = 0;
            IsPlaying = false;
            IsCompleted = false;
            IsKilled = false;
        }

        #endregion
    }
}