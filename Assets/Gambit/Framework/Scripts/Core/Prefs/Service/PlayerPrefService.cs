// PlayerPrefService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Gambit.Framework.Scripts.Core.Prefs.Behaviour;
using Gambit.Framework.Scripts.Core.Prefs.Behaviour.Serializable;
using Gambit.Framework.Scripts.Core.Prefs.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Prefs.Service
{
    [Service]
    public class PlayerPrefService : IPlayerPrefService
    {
        private Dictionary<string, StoredValue> _cache;

        public void OnInit()
        {
            _cache = Load();
        }

        public T GetPref<T>(string key, T value = default)
        {
            return !_cache.TryGetValue(key, out var stored) ? value : stored.Deserialize<T>();
        }

        public void SetPref<T>(string key, T value)
        {
            var store = StoredValue.Create(value);
            if (_cache.TryGetValue(key, out var existing) && existing.Json == store.Json) return;
            _cache[key] = store;
            Save();
        }

        public void Delete(string key)
        {
            if (_cache.Remove(key)) Save();
        }

        public void Save()
        {
            var store = new StoredArray { entries = new StoredEntry[_cache.Count] };
            var pairs = _cache.ToArray();
            for (var i = 0; i < pairs.Length; i++)
                store.entries[i] = new StoredEntry { key = pairs[i].Key, json = pairs[i].Value.Json, type = pairs[i].Value.Name };

            PlayerPrefs.SetString("prefs", JsonUtility.ToJson(store));
            PlayerPrefs.Save();
        }

        public Dictionary<string, StoredValue> Load()
        {
            var result = new Dictionary<string, StoredValue>();
            if (!PlayerPrefs.HasKey("prefs")) return result;
            var store = JsonUtility.FromJson<StoredArray>(PlayerPrefs.GetString("prefs"));
            if (store.entries == null) return result;
            for (var i = 0; i < store.entries.Length; i++)
            {
                var entry = store.entries[i];
                result[entry.key] = new StoredValue(entry.json, entry.type);
            }

            return result;
        }

        public bool HasKey(string key)
        {
            return _cache.ContainsKey(key);
        }
    }
}