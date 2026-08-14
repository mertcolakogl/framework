// Vector2Var.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Variable
{
    [CreateAssetMenu(fileName = "Vector2", menuName = "Gambit/Fischer/Variable/Vector2")]
    public class Vector2Var : GenericVar<Vector2>
    {
        protected override void ResetOnInitialize()
        {
            if (isResetOnInitialize) value = defaultValue;
        }
    }
}