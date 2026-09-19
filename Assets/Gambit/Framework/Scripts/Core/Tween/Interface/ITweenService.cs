// ITweenService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;

namespace Gambit.Framework.Scripts.Core.Tween.Interface
{
    public interface ITweenService : IService
    {
        public void AddTween(ITween tween);

        public void RemoveTween(ITween tween);

        public void Update();
    }
}