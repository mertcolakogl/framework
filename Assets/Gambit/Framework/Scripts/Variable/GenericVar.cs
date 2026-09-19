// GenericVar.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Sirenix.OdinInspector;
using UnityEngine;

namespace Gambit.Framework.Scripts.Variable
{
    public abstract class GenericVar<T> : ScriptableObject
    {
        [SerializeField] protected T defaultValue;
        [ReadOnly] public T value;
        [ReadOnly] public bool isResetOnInitialize = true;

        public T Default
        {
            get => defaultValue;
            set => defaultValue = value;
        }

        private void OnEnable()
        {
            ResetOnInitialize();
        }

        protected abstract void ResetOnInitialize();
    }
}