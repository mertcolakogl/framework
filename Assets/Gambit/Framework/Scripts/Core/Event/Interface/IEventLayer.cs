// IEventLayer.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;

namespace Gambit.Framework.Scripts.Core.Event.Interface
{
    public interface IEventLayer
    {
        public void AddListener(string key, Action<IEvent> e);

        public void RemoveListener(string key, Action<IEvent> e);

        public void Notify(string key, IEvent e = null);
    }
}