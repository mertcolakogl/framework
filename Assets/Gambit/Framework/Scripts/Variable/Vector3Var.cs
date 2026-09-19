// Vector3Var.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Variable
{
    [CreateAssetMenu(fileName = "Vector3", menuName = "Gambit/Fischer/Variable/Vector3")]
    public class Vector3Var : GenericVar<Vector3>
    {
        protected override void ResetOnInitialize()
        {
            if (isResetOnInitialize) value = defaultValue;
        }
    }
}