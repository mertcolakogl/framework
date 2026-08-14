// FloatTweenData.cs
// fischer
//
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Tween.Extension;
using Gambit.Framework.Scripts.Core.Tween.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Tween.Data
{
    [CreateAssetMenu(fileName = "FloatTween", menuName = "Gambit/Fischer/Data/Tween/FloatTweenData")]
    public class FloatTweenData : TweenDataBase
    {
        public ITween TweenFloat(Material m, string propertyName, float from, float to, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return m.TweenFloat(propertyName, from, to)
                .Duration(duration)
                .Delay(delay)
                .Ease(GetEase(easeType))
                .Loop(isLoop ? loopCount : 0)
                .SetLoopType(isLoop ? loopType : default)
                .OnLoopStart(onLoopStart)
                .OnLoopComplete(onLoopComplete)
                .OnStart(onStart)
                .OnComplete(onComplete)
                .Play(null);
        }

        public ITween TweenFloat(Material m, string propertyName, float to, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return m.TweenFloat(propertyName, to)
                .Duration(duration)
                .Delay(delay)
                .Ease(GetEase(easeType))
                .Loop(isLoop ? loopCount : 0)
                .SetLoopType(isLoop ? loopType : default)
                .OnLoopStart(onLoopStart)
                .OnLoopComplete(onLoopComplete)
                .OnStart(onStart)
                .OnComplete(onComplete)
                .Play(null);
        }

        public ITween TweenFloatDynamic(Material m, string propertyName, Func<float> fromFunc, Func<float> toFunc, Action onStart = null,
            Action onComplete = null, Action onLoopStart = null, Action onLoopComplete = null)
        {
            return m.TweenFloatDynamic(propertyName, fromFunc, toFunc)
                .Duration(duration)
                .Delay(delay)
                .Ease(GetEase(easeType))
                .Loop(isLoop ? loopCount : 0)
                .SetLoopType(isLoop ? loopType : default)
                .OnLoopStart(onLoopStart)
                .OnLoopComplete(onLoopComplete)
                .OnStart(onStart)
                .OnComplete(onComplete)
                .Play(null);
        }
    }
}