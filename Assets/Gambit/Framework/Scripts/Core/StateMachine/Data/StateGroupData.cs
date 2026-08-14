// StateGroupData.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.StateMachine.Data
{
    [CreateAssetMenu(fileName = "StateGroup", menuName = "Gambit/Fischer/Data/State/StateGroupData")]
    public class StateGroupData : ScriptableObject
    {
        public List<StateData> states;
    }
}