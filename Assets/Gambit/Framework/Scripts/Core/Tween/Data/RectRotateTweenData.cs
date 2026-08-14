// RectRotateTweenData.cs
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
    [CreateAssetMenu(fileName = "RectRotateTween", menuName = "Gambit/Fischer/Data/Tween/RectRotateTweenData")]
    public class RectRotateTweenData : TweenDataBase
    {
        public ITween TweenRotate(RectTransform rt, Quaternion from, Quaternion to, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return rt.TweenRotate(from, to)
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

        public ITween TweenRotate(RectTransform rt, Quaternion to, Action onStart = null, Action onComplete = null, Action onLoopStart = null,
            Action onLoopComplete = null)
        {
            return rt.TweenRotate(to)
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

        public ITween TweenRotateDynamic(RectTransform rt, Func<Quaternion> fromFunc, Func<Quaternion> toFunc, Action onStart = null,
            Action onComplete = null, Action onLoopStart = null, Action onLoopComplete = null)
        {
            return rt.TweenRotateDynamic(fromFunc, toFunc)
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