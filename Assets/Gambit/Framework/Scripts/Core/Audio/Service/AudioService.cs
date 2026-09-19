// AudioService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Audio.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Other.Utils;
using UnityEngine;

namespace Gambit.Framework.Scripts.Core.Audio.Service
{
    [Service]
    public class AudioService : IAudioService
    {
        private AudioSource _audioSource;

        public void OnInit()
        {
            var obj = Runner.CreateInstance("Audio");
            var cmp = obj.AddComponent<AudioSource>();
            _audioSource = cmp;
        }

        public bool IsActive { get; set; }

        public void SetAudio(bool value)
        {
            IsActive = value;
        }

        public void PlayOneShot(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}