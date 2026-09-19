// TweenDataBase.cs
// fischer
//
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Tween.Behaviour;
using Gambit.Framework.Scripts.Core.Tween.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Tween.Data
{
    public abstract class TweenDataBase : ScriptableObject
    {
        public float duration;
        public float delay;
        public EaseTypes easeType;
        public bool isLoop;
        [ShowIf(nameof(isLoop))] public int loopCount;
        [ShowIf(nameof(isLoop))] public LoopTypes loopType;

        protected static Func<float, float> GetEase(EaseTypes easeType)
        {
            return easeType switch
            {
                EaseTypes.Linear => Ease.Linear,
                EaseTypes.InSine => Ease.InSine,
                EaseTypes.OutSine => Ease.OutSine,
                EaseTypes.InOutSine => Ease.InOutSine,
                EaseTypes.InQuad => Ease.InQuad,
                EaseTypes.OutQuad => Ease.OutQuad,
                EaseTypes.InOutQuad => Ease.InOutQuad,
                EaseTypes.InCubic => Ease.InCubic,
                EaseTypes.OutCubic => Ease.OutCubic,
                EaseTypes.InOutCubic => Ease.InOutCubic,
                EaseTypes.InQuart => Ease.InQuart,
                EaseTypes.OutQuart => Ease.OutQuart,
                EaseTypes.InOutQuart => Ease.InOutQuart,
                EaseTypes.InQuint => Ease.InQuint,
                EaseTypes.OutQuint => Ease.OutQuint,
                EaseTypes.InOutQuint => Ease.InOutQuint,
                EaseTypes.InExpo => Ease.InExpo,
                EaseTypes.OutExpo => Ease.OutExpo,
                EaseTypes.InOutExpo => Ease.InOutExpo,
                EaseTypes.InCirc => Ease.InCirc,
                EaseTypes.OutCirc => Ease.OutCirc,
                EaseTypes.InOutCirc => Ease.InOutCirc,
                EaseTypes.InBack => Ease.InBack,
                EaseTypes.OutBack => Ease.OutBack,
                EaseTypes.InOutBack => Ease.InOutBack,
                EaseTypes.InElastic => Ease.InElastic,
                EaseTypes.OutElastic => Ease.OutElastic,
                EaseTypes.InOutElastic => Ease.InOutElastic,
                _ => throw new ArgumentOutOfRangeException(nameof(easeType), easeType, null)
            };
        }
    }
}