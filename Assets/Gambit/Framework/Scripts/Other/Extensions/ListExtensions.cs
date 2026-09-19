// ListExtensions.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;

namespace Gambit.Framework.Scripts.Other.Extensions
{
    public static class ListExtensions
    {
        public static T GetFirst<T>(this List<T> list)
        {
            return list[0];
        }

        public static bool IsFirst<T>(this List<T> list, T element)
        {
            return list[0].Equals(element);
        }

        public static T GetLast<T>(this List<T> list)
        {
            return list[^1];
        }

        public static bool IsLast<T>(this List<T> list, T element)
        {
            return list[^1].Equals(element);
        }

        public static bool IsLast<T>(this List<T> list, int index)
        {
            return list.Count - 1 == index;
        }
    }
}