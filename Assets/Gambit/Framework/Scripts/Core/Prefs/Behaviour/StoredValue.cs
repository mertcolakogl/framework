// StoredValue.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Prefs.Behaviour.Serializable;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Prefs.Behaviour
{
    public readonly struct StoredValue
    {
        public readonly string Json;
        public readonly string Name;

        public StoredValue(string json, string name)
        {
            Json = json;
            Name = name;
        }

        public static StoredValue Create<T>(T value)
        {
            if (!IsPrimitive(typeof(T))) return new StoredValue(JsonUtility.ToJson(value), typeof(T).AssemblyQualifiedName);
            var wrapped = new PrimitiveWrapper<T> { value = value };
            return new StoredValue(JsonUtility.ToJson(wrapped), typeof(T).AssemblyQualifiedName);
        }

        public T Deserialize<T>()
        {
            if (!IsPrimitive(typeof(T))) return JsonUtility.FromJson<T>(Json);
            var wrapped = JsonUtility.FromJson<PrimitiveWrapper<T>>(Json);
            return wrapped.value;
        }

        private static bool IsPrimitive(Type t)
        {
            return t == typeof(int) || t == typeof(float) || t == typeof(bool) || t == typeof(string) || t == typeof(double) || t == typeof(long);
        }
    }
}