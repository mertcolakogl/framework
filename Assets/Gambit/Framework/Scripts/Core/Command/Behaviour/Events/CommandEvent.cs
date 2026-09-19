// CommandEvent.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Command.Interface;
using UnityEngine.Events;

namespace Gambit.Framework.Scripts.Core.Command.Behaviour.Events
{
    public class CommandEvent : UnityEvent<ICommand>
    {
    }
}