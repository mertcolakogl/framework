// ITweenBuilder.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;

namespace Gambit.Framework.Scripts.Core.Tween.Interface
{
    public interface ITweenBuilder
    {
        public ITween Tween { get; set; }

        public ITween Build();

        public ITween Play(Action<ITween> onPlay);
    }
}