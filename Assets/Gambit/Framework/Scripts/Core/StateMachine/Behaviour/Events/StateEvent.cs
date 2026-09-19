// StateEvent.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine.Events;

namespace Gambit.Framework.Scripts.Core.StateMachine.Behaviour.Events
{
    public class StateEvent
    {
        public readonly UnityEvent OnEnterEvent = new();
        public readonly UnityEvent OnExitEvent = new();
        public readonly UnityEvent OnUpdateEvent = new();
    }
}