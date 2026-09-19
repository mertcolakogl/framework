// PoolService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.Pooling.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Other.Utils;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Pooling.Service
{
    [Service]
    public class PoolService : IPoolService
    {
        private readonly Dictionary<string, Queue<GameObject>> _poolDictionary = new();
        private Transform _root;

        public void OnInit()
        {
            var obj = Runner.CreateInstance("Pool");
            _root = obj.transform;
        }

        public void Create(Queue<GameObject> queue, GameObject obj, int value = 50)
        {
            var wasActive = obj.activeSelf;
            obj.SetActive(false);
            for (var i = 0; i < value; i++)
            {
                var instantiatedGameObject = Object.Instantiate(obj, _root);
                instantiatedGameObject.SetActive(false);
                queue.Enqueue(instantiatedGameObject);
            }

            obj.SetActive(wasActive);
        }

        public GameObject Dequeue(string key, GameObject obj, int value = 50)
        {
            if (!_poolDictionary.ContainsKey(key))
            {
                _poolDictionary[key] = new Queue<GameObject>();
                Create(_poolDictionary[key], obj, value);
            }

            var queue = _poolDictionary[key];
            if (queue.Count == 0) Create(queue, obj, value);
            var result = queue.Dequeue();
            result.SetActive(true);
            return result;
        }

        public void Enqueue(string key, GameObject obj)
        {
            if (obj.transform.parent == _root && !obj.activeSelf) return;
            if (!_poolDictionary.ContainsKey(key)) _poolDictionary[key] = new Queue<GameObject>();
            obj.SetActive(false);
            obj.transform.SetParent(_root);
            _poolDictionary[key].Enqueue(obj);
        }
    }
}