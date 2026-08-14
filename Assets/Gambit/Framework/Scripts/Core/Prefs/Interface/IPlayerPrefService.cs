// IPlayerPrefService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.Prefs.Behaviour;
using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;

namespace Gambit.Framework.Scripts.Core.Prefs.Interface
{
    public interface IPlayerPrefService : IService
    {
        public T GetPref<T>(string key, T value = default);

        public void SetPref<T>(string key, T value);

        public void Delete(string key);

        public void Save();

        public Dictionary<string, StoredValue> Load();

        public bool HasKey(string key);
    }
}