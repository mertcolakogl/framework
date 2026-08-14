// ServiceLocatorInitializer.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using UnityEngine;

namespace Gambit.Framework.Scripts.Core.ServiceLocator.Behaviour
{
    public class ServiceLocatorInitializer : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            var serviceLocator = new GameObject("ServiceLocator");
            serviceLocator.AddComponent<ServiceLocator>();
            DontDestroyOnLoad(serviceLocator);
        }
    }
}