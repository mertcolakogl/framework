// CommandChain.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Gambit.Framework.Scripts.Core.Command.Interface;
using Gambit.Framework.Scripts.Core.Event.Interface;

namespace Gambit.Framework.Scripts.Core.Command.Behaviour
{
    public class CommandChain : CommandBase
    {
        private readonly List<CommandEventPair> _commands = new();

        public void Add(ICommand command, IEvent e = null)
        {
            command.Completed.AddListener(OnSubCommandCompleted);
            _commands.Add(new CommandEventPair { Command = command, E = e });
        }

        private void Next()
        {
            if (_commands.Any())
            {
                var commandEventPair = _commands.FirstOrDefault();
                commandEventPair?.Command.Execute(commandEventPair.E);
            }
            else
            {
                Complete();
            }
        }

        private void OnSubCommandCompleted(ICommand command)
        {
            command.Completed.RemoveListener(OnSubCommandCompleted);
            _commands.RemoveAll(s => s.Command.Equals(command));
            Next();
        }

        protected override void OnExecute(IEvent e = null)
        {
            Next();
        }

        public override void Cancel()
        {
            if (_commands.Any()) _commands.First().Command.Cancel();
            base.Cancel();
        }

        private class CommandEventPair
        {
            public ICommand Command;
            public IEvent E;
        }
    }
}