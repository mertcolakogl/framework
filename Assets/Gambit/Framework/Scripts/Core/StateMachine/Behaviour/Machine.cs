// Machine.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.StateMachine.Enum;
using Gambit.Framework.Scripts.Core.StateMachine.Interface;

namespace Gambit.Framework.Scripts.Core.StateMachine.Behaviour
{
    public class Machine : IMachine
    {
        private readonly Dictionary<string, IState> _states = new();
        public string CurrentKey { get; set; }
        public IState CurrentState { get; set; }

        public void Add(string key, IState state)
        {
            _states.TryAdd(key, state);
        }

        public void ExecuteUpdate()
        {
            if (CurrentState == null) return;
            if ((CurrentState.UpdateType & UpdateTypes.Update) != 0) CurrentState.OnUpdate();
        }

        public void ExecuteFixedUpdate()
        {
            if (CurrentState == null) return;
            if ((CurrentState.UpdateType & UpdateTypes.FixedUpdate) != 0) CurrentState.OnUpdate();
        }

        public void ExecuteLateUpdate()
        {
            if (CurrentState == null) return;
            if ((CurrentState.UpdateType & UpdateTypes.LateUpdate) != 0) CurrentState.OnUpdate();
        }

        public IState Get(string key)
        {
            return _states.GetValueOrDefault(key);
        }

        public void Set(string key)
        {
            if (CurrentKey == key) return;
            CurrentState?.OnExit();
            CurrentState = _states.GetValueOrDefault(key);
            CurrentState?.OnEnter();
            CurrentKey = key;
        }

        public bool Is(string key)
        {
            return _states.TryGetValue(key, out var state) && state.Equals(CurrentState);
        }
    }
}