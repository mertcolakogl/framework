// Resource.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Other.Utils
{
    public static class Resource
    {
        public static T FindSingleAsset<T>(string path) where T : ScriptableObject
        {
            var assets = Resources.LoadAll<T>(path);
            return assets.Length > 0 ? assets[0] : null;
        }
    }
}