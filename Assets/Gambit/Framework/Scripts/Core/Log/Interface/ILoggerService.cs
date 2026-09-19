// ILoggerService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Log.Interface
{
    public interface ILoggerService : IService
    {
        public void DumpHistories();

        public void LogTrace(string msg, GameObject obj);

        public void Log(string msg, GameObject obj);

        public void LogWarning(string msg, GameObject obj);

        public void LogError(string msg, GameObject obj);
    }
}