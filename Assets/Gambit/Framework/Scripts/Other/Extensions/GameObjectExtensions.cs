// GameObjectExtensions.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Other.Extensions
{
    public static class GameObjectExtensions
    {
        public static T Get<T>(this GameObject obj)
        {
            return obj.TryGetComponent<T>(out var component) ? component : default;
        }

        public static T GetParent<T>(this GameObject obj)
        {
            return obj.transform.parent.TryGetComponent<T>(out var component) ? component : default;
        }

        public static bool Ray(this GameObject obj, Vector3 dir, LayerMask mask, out RaycastHit raycastHit, float value = 100f)
        {
            var pos = obj.transform.position;
            if (Physics.Raycast(pos, dir, out var hit, value, mask))
            {
                raycastHit = hit;
                return true;
            }

            raycastHit = default;
            return false;
        }
    }
}