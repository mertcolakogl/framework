// CommandBase.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Command.Behaviour.Events;
using Gambit.Framework.Scripts.Core.Command.Interface;
using Gambit.Framework.Scripts.Core.Event.Behaviour;
using Gambit.Framework.Scripts.Core.Event.Interface;

namespace Gambit.Framework.Scripts.Core.Command.Behaviour
{
    public abstract class CommandBase : EventLayer, ICommand
    {
        #region Interface implementation

        public CommandEvent Completed { get; set; } = new();
        public CommandEvent Canceled { get; set; } = new();

        public void Execute(IEvent e = null)
        {
            OnExecute(e);
        }

        public virtual void Complete()
        {
            Completed.Invoke(this);
        }

        public virtual void Cancel()
        {
            Canceled.Invoke(this);
        }

        protected abstract void OnExecute(IEvent e = null);

        #endregion
    }
}