// IMachine.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

namespace Gambit.Framework.Scripts.Core.StateMachine.Interface
{
    public interface IMachine
    {
        public string CurrentKey { get; set; }

        public IState CurrentState { get; set; }

        public void Add(string key, IState state);

        public void ExecuteUpdate();

        public void ExecuteFixedUpdate();

        public void ExecuteLateUpdate();

        public IState Get(string key);

        public void Set(string key);

        public bool Is(string key);
    }
}