// Ease.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Tween.Behaviour
{
    public static class Ease
    {
        public static float Linear(float t)
        {
            return t;
        }

        public static float InSine(float t)
        {
            return 1f - (float)Math.Cos(t * Math.PI / 2f);
        }

        public static float OutSine(float t)
        {
            return (float)Math.Sin(t * Math.PI / 2f);
        }

        public static float InOutSine(float t)
        {
            return -(float)Math.Cos(Math.PI * t) / 2f + 0.5f;
        }

        public static float InQuad(float t)
        {
            return t * t;
        }

        public static float OutQuad(float t)
        {
            return 1f - (float)Math.Pow(1f - t, 2);
        }

        public static float InOutQuad(float t)
        {
            return EaseMerge(t, InQuad, OutQuad);
        }

        public static float InCubic(float t)
        {
            return t * t * t;
        }

        public static float OutCubic(float t)
        {
            return 1f - (float)Math.Pow(1 - t, 3);
        }

        public static float InOutCubic(float t)
        {
            return EaseMerge(t, InCubic, OutCubic);
        }

        public static float InQuart(float t)
        {
            return t * t * t * t;
        }

        public static float OutQuart(float t)
        {
            return 1f - (float)Math.Pow(1 - t, 4);
        }

        public static float InOutQuart(float t)
        {
            return EaseMerge(t, InQuart, OutQuart);
        }

        public static float InQuint(float t)
        {
            return t * t * t * t * t;
        }

        public static float OutQuint(float t)
        {
            return 1f - (float)Math.Pow(1 - t, 5);
        }

        public static float InOutQuint(float t)
        {
            return EaseMerge(t, InQuint, OutQuint);
        }

        public static float InExpo(float t)
        {
            return t == 0f ? 0f : (float)Math.Pow(2f, 10f * (t - 1f));
        }

        public static float OutExpo(float t)
        {
            return Mathf.Approximately(t, 1f) ? 1f : 1f - (float)Math.Pow(2f, -10f * t);
        }

        public static float InOutExpo(float t)
        {
            return EaseMerge(t, InExpo, OutExpo);
        }

        public static float InCirc(float t)
        {
            return 1f - (float)Math.Sqrt(1f - t * t);
        }

        public static float OutCirc(float t)
        {
            return (float)Math.Sqrt(1f - (t - 1f) * (t - 1f));
        }

        public static float InOutCirc(float t)
        {
            return EaseMerge(t, InCirc, OutCirc);
        }

        public static float InBack(float t)
        {
            return t * t * ((1.70158f + 1f) * t - 1.70158f);
        }

        public static float OutBack(float t)
        {
            return (t - 1f) * (t - 1f) * ((1.70158f + 1f) * (t - 1f) + 1.70158f) + 1f;
        }

        public static float InOutBack(float t)
        {
            return EaseMerge(t, InBack, OutBack);
        }

        public static float InElastic(float t)
        {
            return (float)Math.Sin(13f * Math.PI / 2f * t) * (float)Math.Pow(2f, 10f * (t - 1f));
        }

        public static float OutElastic(float t)
        {
            return (float)Math.Sin(-13f * Math.PI / 2f * (t + 1f)) * (float)Math.Pow(2f, -10f * t) + 1f;
        }

        public static float InOutElastic(float t)
        {
            return EaseMerge(t, InElastic, OutElastic);
        }

        public static float EaseMerge(float t, Func<float, float> firstHalfEase, Func<float, float> lastHalfEase)
        {
            return t < 0.5f ? firstHalfEase(t * 2) / 2f : 0.5f + lastHalfEase((t - 0.5f) * 2) / 2f;
        }
    }
}