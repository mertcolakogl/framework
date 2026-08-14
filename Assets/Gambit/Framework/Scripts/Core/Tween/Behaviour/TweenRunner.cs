// TweenRunner.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Tween.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Tween.Behaviour
{
    public class TweenRunner : MonoBehaviour, ITweenRunner
    {
        private void Update()
        {
            TweenService?.Update();
        }

        #region Interface implementation

        public ITweenService TweenService { get; set; }

        public void OnRun(ITweenService tweenService)
        {
            TweenService = tweenService;
        }

        #endregion
    }
}