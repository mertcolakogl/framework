// BindEventAttribute.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;

namespace Gambit.Framework.Scripts.Core.Event.Attributes
{
    public class BindEventAttribute : Attribute
    {
        public BindEventAttribute(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; }
    }
}