// EventDispatcherService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.Event.Behaviour.Events;
using Gambit.Framework.Scripts.Core.Event.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;

namespace Gambit.Framework.Scripts.Core.Event.Service
{
    [Service]
    public class EventDispatcherService : IEventDispatcherService
    {
        private readonly Dictionary<string, GameEvent> _gameEvents = new();

        public void OnInit()
        {
        }

        public void AddListener(string key, Action<IEvent> e)
        {
            _gameEvents.TryAdd(key, new GameEvent());
            _gameEvents[key].AddListener(e.Invoke);
        }

        public void RemoveListener(string key, Action<IEvent> e)
        {
            if (_gameEvents.TryGetValue(key, out var gameEvent)) gameEvent.RemoveListener(e.Invoke);
        }

        public void Notify(string key, IEvent e = null)
        {
            if (_gameEvents.TryGetValue(key, out var gameEvent)) gameEvent.Invoke(e);
        }
    }
}