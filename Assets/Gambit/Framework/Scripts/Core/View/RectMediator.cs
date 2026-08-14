// RectMediator.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Event.Interface;
using Gambit.Framework.Scripts.Core.Pooling.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.View
{
    public class RectMediator : MediatorBase
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
            if (gameObject.activeSelf) Of<IRectService>().Enqueue(Key, gameObject);
        }
    }
}