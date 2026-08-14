// StateRunner.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.StateMachine.Data;
using Gambit.Framework.Scripts.Core.StateMachine.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.StateMachine.Behaviour
{
    public class StateRunner : MonoBehaviour, IStateRunner
    {
        [ReadOnly] public StateData state;

        private void Update()
        {
            Machine.ExecuteUpdate();
        }

        private void FixedUpdate()
        {
            Machine.ExecuteFixedUpdate();
        }

        private void LateUpdate()
        {
            Machine.ExecuteLateUpdate();
        }

        #region Interface implementation

        public IMachine Machine { get; set; }

        public void OnRun(StateData stateData, IMachine machine)
        {
            state = stateData;
            Machine = machine;
            for (var i = 0; i < state.states.Count; i++) Machine.Add(state.states[i].name, new State(state.states[i].updateType));
            Machine.Set(state.states[0].name);
        }

        #endregion
    }
}