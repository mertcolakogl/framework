// ITween.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using System;

namespace Gambit.Framework.Scripts.Core.Tween.Interface
{
    public interface ITween
    {
        public bool IsPlaying { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsKilled { get; set; }

        public int Loop { get; set; }

        public int ElapsedLoop { get; set; }

        public Action OnStart { get; set; }

        public Action OnComplete { get; set; }

        public Action OnLoopStart { get; set; }

        public Action OnLoopComplete { get; set; }

        public void Update(float deltaTime, Action<ITween> onUpdate = null);

        public void Play(Action<ITween> onPlay = null);

        public void Kill(Action<ITween> onKill = null);

        public void Reset();
    }
}