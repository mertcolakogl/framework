// Singleton.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Singleton
{
    public class Singleton<T> : SingletonBase where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (IsApplicationQuit) return null;
                lock (Lock)
                {
                    return _instance ??= (T)FindFirstObjectByType(typeof(T));
                }
            }
        }
    }
}