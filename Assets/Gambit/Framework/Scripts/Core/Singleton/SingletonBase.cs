// SingletonBase.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Singleton
{
    public class SingletonBase : MonoBehaviour
    {
        protected static readonly object Lock = new();
        protected static bool IsApplicationQuit;

        private void OnApplicationQuit()
        {
            IsApplicationQuit = true;
        }
    }
}