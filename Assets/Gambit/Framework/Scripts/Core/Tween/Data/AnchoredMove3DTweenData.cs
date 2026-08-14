// AnchoredMove3DTweenData.cs
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
    [CreateAssetMenu(fileName = "AnchoredMove3DTween", menuName = "Gambit/Fischer/Data/Tween/AnchoredMove3DTweenData")]
    public class AnchoredMove3DTweenData : TweenDataBase
    {
        public ITween TweenAnchoredMove3D(RectTransform rt, Vector3 from, Vector3 to, Action onStart = null, Action onComplete = null,
            Action onLoopStart = null, Action onLoopComplete = null)
        {
            return rt.TweenAnchoredMove3D(from, to)
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

        public ITween TweenAnchoredMove3D(RectTransform rt, Vector3 to, Action onStart = null, Action onComplete = null, Action onLoopStart = null,
            Action onLoopComplete = null)
        {
            return rt.TweenAnchoredMove3D(to)
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

        public ITween TweenAnchoredMove3DDynamic(RectTransform rt, Func<Vector3> fromFunc, Func<Vector3> toFunc, Action onStart = null,
            Action onComplete = null, Action onLoopStart = null, Action onLoopComplete = null)
        {
            return rt.TweenAnchoredMove3DDynamic(fromFunc, toFunc)
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