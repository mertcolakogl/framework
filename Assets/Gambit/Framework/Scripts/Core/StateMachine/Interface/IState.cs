// IState.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.StateMachine.Behaviour.Events;
using Gambit.Framework.Scripts.Core.StateMachine.Enum;

namespace Gambit.Framework.Scripts.Core.StateMachine.Interface
{
    public interface IState
    {
        public StateEvent Event { get; set; }

        public UpdateTypes UpdateType { get; }

        public void OnEnter();

        public void OnExit();

        public void OnUpdate();
    }
}