// IAsyncService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using System.Collections;
using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Async.Interface
{
    public interface IAsyncService : IService
    {
        public void ExecuteCoroutine(IEnumerator action);

        public void ExecuteInUpdate(Action action);

        public void ExecuteInFixedUpdate(Action action);

        public void ExecuteInLateUpdate(Action action);

        public Coroutine ExecuteAsyncOperation(AsyncOperation async);

        public Coroutine WaitForEndOfFrame(Action onComplete);

        public Coroutine WaitForSecond(Action onComplete, float seconds);
    }
}