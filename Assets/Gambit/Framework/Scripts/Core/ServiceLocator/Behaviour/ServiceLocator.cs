// ServiceLocator.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;
using Gambit.Framework.Scripts.Core.Singleton;
using Gambit.Framework.Scripts.Core.View;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.ServiceLocator.Behaviour
{
    public class ServiceLocator : Singleton<ServiceLocator>, IServiceLayer
    {
        private readonly Dictionary<Type, Type> _allServices = new();
        private readonly Dictionary<Type, IService> _runningServices = new();

        private void Awake()
        {
            RegisterAllServices();
            RegisterNotLazyServices();
            GameBoot();
        }

        #region Interface implementation

        public T Of<T>() where T : IService
        {
            var service = typeof(T);
            if (!_runningServices.ContainsKey(service)) RegisterService(service);
            return (T)_runningServices[service];
        }

        #endregion

        private static void GameBoot()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.Select(a => a.GetTypes()).SelectMany(b => b)
                .Where(c => c.GetCustomAttributes(typeof(BootGameAttribute), false).Length > 0).ToList();
            Activator.CreateInstance(types.First());
        }

        private void RegisterAllServices()
        {
            _allServices.Clear();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.Select(a => a.GetTypes()).SelectMany(t => t)
                .Where(s => s.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0).ToList();
            for (var i = 0; i < types.Count; i++)
            {
                var interfaceTypes = types[i].GetInterfaces();
                var selectedTypes = interfaceTypes.Where(t => t.GetInterface(nameof(IService)) != null).ToList();
                _allServices.Add(selectedTypes.First(), types[i]);
            }
        }

        private void RegisterNotLazyServices()
        {
            _runningServices.Clear();
            var services = _allServices
                .Where(t => t.Value.GetCustomAttribute(typeof(ServiceAttribute)) is ServiceAttribute { IsLazy: false })
                .ToList();
            for (var i = 0; i < services.Count; i++) RegisterService(services[i].Key);
        }

        private void RegisterService(Type type)
        {
            if (_runningServices.ContainsKey(type)) return;
            var actualType = _allServices[type];
            if (typeof(MonoBehaviour).IsAssignableFrom(actualType))
            {
                if (typeof(NodeServiceBase).IsAssignableFrom(actualType))
                {
                    StartCoroutine(AddServiceCoroutine(type, actualType));
                    return;
                }

                var serviceGameObject = new GameObject { name = actualType.Name };
                var service = serviceGameObject.AddComponent(actualType) as IService;
                AddService(type, service);
                DontDestroyOnLoad(serviceGameObject);
                return;
            }

            AddService(type, (IService)Activator.CreateInstance(actualType));
        }

        private void AddService(Type type, IService service)
        {
            _runningServices.Add(type, service);
            service?.OnInit();
        }

        private IEnumerator AddServiceCoroutine(Type type, Type actualType)
        {
            yield return new WaitForEndOfFrame();
            var service = FindFirstObjectByType(actualType);
            if (service != null) AddService(type, (IService)service);
        }
    }
}