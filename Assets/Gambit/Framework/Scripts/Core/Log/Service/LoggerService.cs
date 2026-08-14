// LoggerService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gambit.Framework.Scripts.Core.Log.Enum;
using Gambit.Framework.Scripts.Core.Log.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Log.Service
{
    [Service]
    public class LoggerService : ILoggerService
    {
        private readonly Stack<LogHistory> _histories = new();
        private readonly LogTypes _logType = LogTypes.Info;
        
        public void OnInit()
        {
        }

        public void DumpHistories()
        {
#if UNITY_EDITOR
            if (_histories.Count == 0) return;
            var sb = new StringBuilder();
            foreach (var history in _histories.Reverse())
            {
                var hex = GetColorForLogType(history.logType);
                sb.AppendLine($"<color={hex}>[{history.time:F2}s] [{history.logType}] {history.msg}</color>");
            }

            Debug.Log(sb.ToString());
#endif
        }

        public void LogTrace(string msg, GameObject obj = null)
        {
            PushHistory(msg, LogTypes.Verbose);
            if (_logType >= LogTypes.Verbose) Debug.Log(msg, obj);
        }

        public void Log(string msg, GameObject obj = null)
        {
            PushHistory(msg, LogTypes.Info);
            if (_logType >= LogTypes.Info) Debug.Log(msg, obj);
        }

        public void LogWarning(string msg, GameObject obj = null)
        {
            PushHistory(msg, LogTypes.Warning);
            if (_logType >= LogTypes.Warning) Debug.LogWarning(msg, obj);
        }

        public void LogError(string msg, GameObject obj = null)
        {
            PushHistory(msg + "\n" + StackTraceUtility.ExtractStackTrace(), LogTypes.Error);
            Debug.LogError(msg, obj);
        }

        private string GetColorForLogType(LogTypes type)
        {
            return type switch
            {
                LogTypes.Error => "#9E9E9E",
                LogTypes.Warning => "#FFFFFF",
                LogTypes.Info => "#FFD500",
                LogTypes.Verbose => "#FF5252",
                _ => "#FFFFFF"
            };
        }

        private void PushHistory(string msg, LogTypes type)
        {
            var history = new LogHistory { logType = type, msg = msg, time = Time.time };
            _histories.Push(history);
        }

        [Serializable]
        private class LogHistory
        {
            public LogTypes logType;
            public string msg;
            public float time;
        }
    }
}