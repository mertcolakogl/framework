// StateData.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.StateMachine.Data.Serializable;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.StateMachine.Data
{
    [CreateAssetMenu(fileName = "State", menuName = "Gambit/Fischer/Data/State/StateData")]
    public class StateData : ScriptableObject
    {
        public string key;
        public List<StateEntry> states;
    }
}