// Runner.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Other.Utils
{
    public static class Runner
    {
        public static GameObject CreateInstance(string key)
        {
            var obj = new GameObject(key + "Runner");
            Object.DontDestroyOnLoad(obj);
            return obj;
        }
    }
}