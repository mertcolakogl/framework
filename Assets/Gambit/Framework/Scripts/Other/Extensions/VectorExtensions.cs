// VectorExtensions.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Other.Extensions
{
    public static class VectorExtensions
    {
        public static bool Ray(this Vector3 transform, float maxDistance, LayerMask mask, out RaycastHit raycastHit)
        {
            if (Camera.main)
            {
                var isHit = Camera.main.ScreenPointToRay(transform).Ray(maxDistance, mask, out var hit);
                raycastHit = isHit ? hit : default;
                return isHit;
            }

            raycastHit = default;
            return false;
        }

        private static bool Ray(this Ray ray, float maxDistance, LayerMask mask, out RaycastHit raycastHit)
        {
            if (Physics.Raycast(ray, out var hit, maxDistance, mask))
            {
                raycastHit = hit;
                return true;
            }

            raycastHit = default;
            return false;
        }
    }
}