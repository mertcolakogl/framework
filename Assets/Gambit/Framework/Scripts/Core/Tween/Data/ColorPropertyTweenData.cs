// ColorPropertyTweenData.cs
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
    [CreateAssetMenu(fileName = "ColorPropertyTween", menuName = "Gambit/Fischer/Data/Tween/ColorPropertyTweenData")]
    public class ColorPropertyTweenData : TweenDataBase
    {
        public ITween TweenColor(Material m, string propertyName, Color from, Color to, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return m.TweenColor(propertyName, from, to)
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

        public ITween TweenColor(Material m, string propertyName, Color to, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return m.TweenColor(propertyName, to)
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

        public ITween TweenColorDynamic(Material m, string propertyName, Func<Color> fromFunc, Func<Color> toFunc, Action onStart = null,
            Action onComplete = null, Action onLoopStart = null, Action onLoopComplete = null)
        {
            return m.TweenColorDynamic(propertyName, fromFunc, toFunc)
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