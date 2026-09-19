// CommandService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;
using System.Linq;
using Gambit.Framework.Scripts.Core.Command.Interface;
using Gambit.Framework.Scripts.Core.Event.Attributes;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Core.View;

namespace Gambit.Framework.Scripts.Core.Command.Service
{
    [Service]
    public class CommandService : ServiceBase, ICommandService
    {
        public void OnInit()
        {
            BindEventToCommand();
        }

        private void BindEventToCommand()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.Select(a => a.GetTypes()).SelectMany(b => b)
                .Where(c => c.GetCustomAttributes(typeof(BindEventAttribute), false).Length > 0).ToList();

            for (var i = 0; i < types.Count; i++)
            {
                var type = types[i];
                var attr = type.GetCustomAttributes(typeof(BindEventAttribute), false).First() as BindEventAttribute;
                AddListener(attr?.EventName, e => { (Activator.CreateInstance(type) as ICommand)?.Execute(e); });
            }
        }
    }
}