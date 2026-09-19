// BoolVar.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Variable
{
    [CreateAssetMenu(fileName = "Bool", menuName = "Gambit/Fischer/Variable/Bool")]
    public class BoolVar : GenericVar<bool>
    {
        protected override void ResetOnInitialize()
        {
            if (isResetOnInitialize) value = defaultValue;
        }
    }
}