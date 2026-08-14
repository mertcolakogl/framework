// IStateService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;

namespace Gambit.Framework.Scripts.Core.StateMachine.Interface
{
    public interface IStateService : IService
    {
        public IState GetState(string key, string state);

        public IState GetState(string key, int index);

        public void SetState(string key, string state);

        public void SetState(string key, int index);
    }
}