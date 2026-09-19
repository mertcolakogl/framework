// PoolMediator.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Event.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.View
{
    public class PoolMediator : MediatorBase
    {
        [SerializeField] private MediatorBase mediator;
        [SerializeField] private int id;

        public string Key => mediator.GetType().Name + id;

        protected override void AddListeners()
        {
            AddListener("OnLoadScene", OnLoadScene);
        }

        protected override void RemoveListeners()
        {
            RemoveListener("OnLoadScene", OnLoadScene);
        }

        private void OnLoadScene(IEvent e = null)
        {
            if (gameObject.activeSelf) Enqueue(gameObject);
        }
    }
}