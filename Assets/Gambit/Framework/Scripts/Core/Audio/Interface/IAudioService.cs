// IAudioService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.ServiceLocator.Interface;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Audio.Interface
{
    public interface IAudioService : IService
    {
        public bool IsActive { get; set; }

        public void SetAudio(bool value);

        public void PlayOneShot(AudioClip clip);
    }
}