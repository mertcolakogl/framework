// ITweenRunner.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

namespace Gambit.Framework.Scripts.Core.Tween.Interface
{
    public interface ITweenRunner
    {
        public ITweenService TweenService { get; set; }

        public void OnRun(ITweenService tweenService);
    }
}