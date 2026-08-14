// ICommand.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Command.Behaviour.Events;
using Gambit.Framework.Scripts.Core.Event.Interface;

namespace Gambit.Framework.Scripts.Core.Command.Interface
{
    public interface ICommand
    {
        public CommandEvent Completed { get; set; }

        public CommandEvent Canceled { get; set; }

        public void Execute(IEvent e = null);

        public void Complete();

        public void Cancel();
    }
}