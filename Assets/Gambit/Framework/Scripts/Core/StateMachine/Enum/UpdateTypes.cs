// UpdateTypes.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;

namespace Gambit.Framework.Scripts.Core.StateMachine.Enum
{
    [Flags]
    public enum UpdateTypes : byte
    {
        None = 0,
        Update = 1 << 0,
        FixedUpdate = 1 << 1,
        LateUpdate = 1 << 2
    }
}