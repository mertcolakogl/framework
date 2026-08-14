// TweenService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Core.Tween.Behaviour;
using Gambit.Framework.Scripts.Core.Tween.Interface;
using Gambit.Framework.Scripts.Other.Utils;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Tween.Service
{
    [Service]
    public class TweenService : ITweenService
    {
        private readonly List<ITween> _activeTweenList = new();
        private int _activeTweenCount;

        public void OnInit()
        {
            var obj = Runner.CreateInstance("Tween");
            var cmp = obj.AddComponent<TweenRunner>();
            cmp.OnRun(this);
        }

        public void AddTween(ITween tween)
        {
            if (!_activeTweenList.Contains(tween)) _activeTweenList.Add(tween);
        }

        public void RemoveTween(ITween tween)
        {
            _activeTweenList.Remove(tween);
        }

        public void Update()
        {
            _activeTweenCount = _activeTweenList.Count;
            for (var i = 0; i < _activeTweenList.Count; i++)
            {
                var activeTween = _activeTweenList[i];
                if (activeTween.IsPlaying) activeTween.Update(Time.deltaTime, AddTween);
                else _activeTweenList.Remove(activeTween);
            }
        }
    }
}