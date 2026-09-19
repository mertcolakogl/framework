// StateEntry.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.StateMachine.Enum;

namespace Gambit.Framework.Scripts.Core.StateMachine.Data.Serializable
{
    [Serializable]
    public class StateEntry
    {
        public string name;
        public UpdateTypes updateType;
    }
}