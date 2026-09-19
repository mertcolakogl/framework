// CommandGroup.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using Gambit.Framework.Scripts.Core.Command.Interface;
using Gambit.Framework.Scripts.Core.Event.Interface;

namespace Gambit.Framework.Scripts.Core.Command.Behaviour
{
    public class CommandGroup : CommandBase
    {
        private readonly List<CommandEventPair> _commands = new();
        private int _completedCount;

        public void Add(ICommand command, IEvent e = null)
        {
            command.Completed.AddListener(OnSubCommandCompleted);
            _commands.Add(new CommandEventPair { Command = command, E = e });
        }

        private void OnSubCommandCompleted(ICommand command)
        {
            command.Completed.RemoveListener(OnSubCommandCompleted);
            _completedCount++;
            if (_completedCount == _commands.Count) Complete();
        }

        protected override void OnExecute(IEvent e = null)
        {
            for (var i = 0; i < _commands.Count; i++) _commands[i].Command.Execute(_commands[i].E);
        }

        private class CommandEventPair
        {
            public ICommand Command;
            public IEvent E;
        }
    }
}