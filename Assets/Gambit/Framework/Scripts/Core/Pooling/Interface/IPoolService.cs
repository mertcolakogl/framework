// IPoolService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Pooling.Interface
{
    public interface IPoolService : IService
    {
        public void Create(Queue<GameObject> queue, GameObject obj, int value = 50);

        public GameObject Dequeue(string key, GameObject obj, int value = 50);

        public void Enqueue(string key, GameObject obj);
    }
}