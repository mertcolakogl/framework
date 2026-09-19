// TweenBuilder.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Tween.Enum;
using Gambit.Framework.Scripts.Core.Tween.Interface;

namespace Gambit.Framework.Scripts.Core.Tween.Behaviour
{
    public class TweenBuilder : ITweenBuilder
    {
        public TweenBuilder Duration(float duration)
        {
            var tween = (TweenBase)Tween;
            tween.Duration = duration;
            return this;
        }

        public TweenBuilder Delay(float delay)
        {
            var tween = (TweenBase)Tween;
            tween.Delay = delay;
            return this;
        }

        public TweenBuilder Ease(Func<float, float> easeFunc)
        {
            var tween = (TweenBase)Tween;
            tween.EaseFunc = easeFunc;
            return this;
        }

        public TweenBuilder Loop(int loop)
        {
            var tween = (TweenBase)Tween;
            tween.Loop = loop;
            return this;
        }

        public TweenBuilder SetLoopType(LoopTypes loopType)
        {
            var tween = (TweenBase)Tween;
            tween.LoopType = loopType;
            return this;
        }

        public TweenBuilder OnTweenAction(Action<float, float> onTweenAction)
        {
            var tween = (TweenBase)Tween;
            tween.OnTweenAction = onTweenAction;
            return this;
        }

        public TweenBuilder OnStart(Action onStart)
        {
            var tween = (TweenBase)Tween;
            tween.OnStart = onStart;
            return this;
        }

        public TweenBuilder OnLoopStart(Action onStart)
        {
            var tween = (TweenBase)Tween;
            tween.OnLoopStart = onStart;
            return this;
        }

        public TweenBuilder OnComplete(Action onComplete)
        {
            var tween = (TweenBase)Tween;
            tween.OnComplete = onComplete;
            return this;
        }

        public TweenBuilder OnLoopComplete(Action onComplete)
        {
            var tween = (TweenBase)Tween;
            tween.OnLoopComplete = onComplete;
            return this;
        }

        public TweenBuilder OnUpdate(Action<float, float> onUpdate)
        {
            var tween = (TweenBase)Tween;
            tween.OnUpdate = onUpdate;
            return this;
        }

        #region Interface implementation

        public ITween Tween { get; set; }

        public ITween Build()
        {
            return Tween;
        }

        public ITween Play(Action<ITween> onPlay)
        {
            Tween.Play(onPlay);
            return Tween;
        }

        #endregion
    }
}