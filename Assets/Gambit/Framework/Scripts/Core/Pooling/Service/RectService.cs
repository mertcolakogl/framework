// RectService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.Pooling.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Core.View;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Pooling.Service
{
    [Service]
    public class RectService : NodeServiceBase, IRectService
    {
        private readonly Dictionary<string, Queue<GameObject>> _rectDictionary = new();

        public void OnInit()
        {
        }

        public void Create(Queue<GameObject> queue, GameObject obj, int value = 50)
        {
            for (var i = 0; i < value; i++)
            {
                var instantiatedGameObject = Instantiate(obj, transform);
                instantiatedGameObject.SetActive(false);
                queue.Enqueue(instantiatedGameObject);
            }
        }

        public GameObject Dequeue(string key, GameObject obj, int value = 50)
        {
            if (!_rectDictionary.ContainsKey(key))
            {
                _rectDictionary[key] = new Queue<GameObject>();
                Create(_rectDictionary[key], obj, value);
            }

            var queue = _rectDictionary[key];
            if (queue.Count == 0) Create(queue, obj, value);
            var result = queue.Dequeue();
            result.SetActive(true);
            return result;
        }

        public void Enqueue(string key, GameObject obj)
        {
            if (obj.transform.parent == transform && !obj.activeSelf) return;
            if (!_rectDictionary.ContainsKey(key)) _rectDictionary[key] = new Queue<GameObject>();
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            _rectDictionary[key].Enqueue(obj);
        }
    }
}