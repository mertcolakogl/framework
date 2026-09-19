// StateService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Core.StateMachine.Behaviour;
using Gambit.Framework.Scripts.Core.StateMachine.Data;
using Gambit.Framework.Scripts.Core.StateMachine.Interface;
using Gambit.Framework.Scripts.Other.Utils;

namespace Gambit.Framework.Scripts.Core.StateMachine.Service
{
    [Service]
    public class StateService : IStateService
    {
        private readonly Dictionary<string, (IMachine machine, StateData data)> _stateMachines = new();
        private StateGroupData _stateGroup;

        public void OnInit()
        {
            _stateGroup = Resource.FindSingleAsset<StateGroupData>(Path.Data);
            for (var i = 0; i < _stateGroup.states.Count; i++)
            {
                var state = _stateGroup.states[i];
                var machine = new Machine();
                _stateMachines.Add(state.key, (machine, state));
                var obj = Runner.CreateInstance(state.key);
                var cmp = obj.AddComponent<StateRunner>();
                cmp.OnRun(state, machine);
            }
        }

        public IState GetState(string key, string state)
        {
            return _stateMachines[key].machine.Get(state);
        }

        public IState GetState(string key, int index)
        {
            var entry = _stateMachines[key];
            return entry.machine.Get(entry.data.states[index].name);
        }

        public void SetState(string key, string state)
        {
            _stateMachines[key].machine.Set(state);
        }

        public void SetState(string key, int index)
        {
            var entry = _stateMachines[key];
            entry.machine.Set(entry.data.states[index].name);
        }
    }
}