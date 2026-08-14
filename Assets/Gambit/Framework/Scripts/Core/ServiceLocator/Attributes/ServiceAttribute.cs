// ServiceAttribute.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;

namespace Gambit.Framework.Scripts.Core.ServiceLocator.Attributes
{
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(bool isLazy = false)
        {
            IsLazy = isLazy;
        }

        public bool IsLazy { get; }
    }
}