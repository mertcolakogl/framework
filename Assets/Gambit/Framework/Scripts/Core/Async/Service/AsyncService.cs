// AsyncService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.Async.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Core.View;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Async.Service
{
    [Service]
    public class AsyncService : MonoServiceBase, IAsyncService
    {
        private readonly List<Action> _actionCopiedQueueFixedUpdateFunc = new();
        private readonly List<Action> _actionCopiedQueueLateUpdateFunc = new();
        private readonly List<Action> _actionCopiedQueueUpdateFunc = new();
        private readonly List<Action> _actionQueuesFixedUpdateFunc = new();
        private readonly List<Action> _actionQueuesLateUpdateFunc = new();
        private readonly List<Action> _actionQueuesUpdateFunc = new();
        private volatile bool _noActionQueueToExecuteFixedUpdateFunc = true;
        private volatile bool _noActionQueueToExecuteLateUpdateFunc = true;
        private volatile bool _noActionQueueToExecuteUpdateFunc = true;

        private void Update()
        {
            if (_noActionQueueToExecuteUpdateFunc) return;
            _actionCopiedQueueUpdateFunc.Clear();
            lock (_actionQueuesUpdateFunc)
            {
                _actionCopiedQueueUpdateFunc.AddRange(_actionQueuesUpdateFunc);
                _actionQueuesUpdateFunc.Clear();
                _noActionQueueToExecuteUpdateFunc = true;
            }

            for (var i = 0; i < _actionCopiedQueueUpdateFunc.Count; i++) _actionCopiedQueueUpdateFunc[i].Invoke();
        }

        private void FixedUpdate()
        {
            if (_noActionQueueToExecuteFixedUpdateFunc) return;
            _actionCopiedQueueFixedUpdateFunc.Clear();
            lock (_actionQueuesFixedUpdateFunc)
            {
                _actionCopiedQueueFixedUpdateFunc.AddRange(_actionQueuesFixedUpdateFunc);
                _actionQueuesFixedUpdateFunc.Clear();
                _noActionQueueToExecuteFixedUpdateFunc = true;
            }

            for (var i = 0; i < _actionCopiedQueueFixedUpdateFunc.Count; i++)
                _actionCopiedQueueFixedUpdateFunc[i].Invoke();
        }

        private void LateUpdate()
        {
            if (_noActionQueueToExecuteLateUpdateFunc) return;
            _actionCopiedQueueLateUpdateFunc.Clear();
            lock (_actionQueuesLateUpdateFunc)
            {
                _actionCopiedQueueLateUpdateFunc.AddRange(_actionQueuesLateUpdateFunc);
                _actionQueuesLateUpdateFunc.Clear();
                _noActionQueueToExecuteLateUpdateFunc = true;
            }

            for (var i = 0; i < _actionCopiedQueueLateUpdateFunc.Count; i++)
                _actionCopiedQueueLateUpdateFunc[i].Invoke();
        }

        private static IEnumerator ExecuteAsyncOperationCoroutine(AsyncOperation async)
        {
            while (!async.isDone) yield return null;
        }

        private static IEnumerator WaitForEndOfFrameCoroutine(Action action)
        {
            yield return new WaitForEndOfFrame();
            action.Invoke();
        }

        private static IEnumerator WaitForSecondCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke();
        }

        #region Interface implementation

        public void OnInit()
        {
        }

        public void ExecuteCoroutine(IEnumerator action)
        {
            ExecuteInUpdate(() => StartCoroutine(action));
        }

        public void ExecuteInUpdate(Action action)
        {
            lock (_actionQueuesUpdateFunc)
            {
                _actionQueuesUpdateFunc.Add(action);
                _noActionQueueToExecuteUpdateFunc = false;
            }
        }

        public void ExecuteInFixedUpdate(Action action)
        {
            lock (_actionQueuesFixedUpdateFunc)
            {
                _actionQueuesFixedUpdateFunc.Add(action);
                _noActionQueueToExecuteFixedUpdateFunc = false;
            }
        }

        public void ExecuteInLateUpdate(Action action)
        {
            lock (_actionQueuesLateUpdateFunc)
            {
                _actionQueuesLateUpdateFunc.Add(action);
                _noActionQueueToExecuteLateUpdateFunc = false;
            }
        }

        public Coroutine ExecuteAsyncOperation(AsyncOperation async)
        {
            return StartCoroutine(ExecuteAsyncOperationCoroutine(async));
        }

        public Coroutine WaitForEndOfFrame(Action onComplete)
        {
            return StartCoroutine(WaitForEndOfFrameCoroutine(onComplete));
        }

        public Coroutine WaitForSecond(Action onComplete, float seconds)
        {
            return StartCoroutine(WaitForSecondCoroutine(seconds, onComplete));
        }

        #endregion
    }
}