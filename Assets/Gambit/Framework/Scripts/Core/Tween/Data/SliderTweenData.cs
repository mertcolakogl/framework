// SliderTweenData.cs
// fischer
//
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Tween.Extension;
using Gambit.Framework.Scripts.Core.Tween.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Gambit.Framework.Scripts.Core.Tween.Data
{
    [CreateAssetMenu(fileName = "SliderTween", menuName = "Gambit/Fischer/Data/Tween/SliderTweenData")]
    public class SliderTweenData : TweenDataBase
    {
        public ITween TweenSlider(Slider s, float from, float to, Action onStart = null, Action onComplete = null, Action onLoopStart = null,
            Action onLoopComplete = null)
        {
            return s.TweenSlider(from, to)
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

        public ITween TweenSlider(Slider s, float to, Action onStart = null, Action onComplete = null, Action onLoopStart = null,
            Action onLoopComplete = null)
        {
            return s.TweenSlider(to)
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

        public ITween TweenSliderDynamic(Slider s, Func<float> fromFunc, Func<float> toFunc, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return s.TweenSliderDynamic(fromFunc, toFunc)
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