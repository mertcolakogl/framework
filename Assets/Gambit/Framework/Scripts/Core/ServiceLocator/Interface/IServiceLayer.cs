// IServiceLayer.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

namespace Gambit.Framework.Scripts.Core.ServiceLocator.Interface
{
    public interface IServiceLayer
    {
        public T Of<T>() where T : IService;
    }
}