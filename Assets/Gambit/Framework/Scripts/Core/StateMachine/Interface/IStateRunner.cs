// IStateRunner.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.StateMachine.Data;

namespace Gambit.Framework.Scripts.Core.StateMachine.Interface
{
    public interface IStateRunner
    {
        public IMachine Machine { get; set; }

        public void OnRun(StateData stateData, IMachine machine);
    }
}