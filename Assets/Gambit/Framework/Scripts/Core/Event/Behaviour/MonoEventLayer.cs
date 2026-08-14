// MonoEventLayer.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using Gambit.Framework.Scripts.Core.Event.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Behaviour;

namespace Gambit.Framework.Scripts.Core.Event.Behaviour
{
    public class MonoEventLayer : MonoServiceLayer, IEventLayer
    {
        private IEventDispatcherService _eventDispatcher;

        private IEventDispatcherService EventDispatcher
        {
            get { return _eventDispatcher ??= Of<IEventDispatcherService>(); }
        }

        #region Interface implementation

        public void AddListener(string key, Action<IEvent> e)
        {
            EventDispatcher.AddListener(key, e);
        }

        public void RemoveListener(string key, Action<IEvent> e)
        {
            EventDispatcher.RemoveListener(key, e);
        }

        public void Notify(string key, IEvent e = null)
        {
            EventDispatcher.Notify(key, e);
        }

        #endregion
    }
}