// State.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.StateMachine.Behaviour.Events;
using Gambit.Framework.Scripts.Core.StateMachine.Enum;
using Gambit.Framework.Scripts.Core.StateMachine.Interface;

namespace Gambit.Framework.Scripts.Core.StateMachine.Behaviour
{
    public class State : IState
    {
        public State(UpdateTypes updateType)
        {
            Event = new StateEvent();
            UpdateType = updateType;
        }

        public StateEvent Event { get; set; }
        public UpdateTypes UpdateType { get; }

        public void OnEnter()
        {
            Event.OnEnterEvent?.Invoke();
        }

        public void OnExit()
        {
            Event.OnExitEvent?.Invoke();
        }

        public void OnUpdate()
        {
            Event.OnUpdateEvent?.Invoke();
        }
    }
}