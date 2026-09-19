// HapticService.cs
// framework
// 
// Created by mert on 01.01.2026.
// Copyright (c) 2026 Mert Colakoglu. All rights reserved.

using Gambit.Framework.Scripts.Core.Haptic.Enum;
using Gambit.Framework.Scripts.Core.Haptic.Interface;
using Gambit.Framework.Scripts.Core.ServiceLocator.Attributes;
using Gambit.Framework.Scripts.Other.Utils;

namespace Gambit.Framework.Scripts.Core.Haptic.Service
{
    [Service]
    public class HapticService : IHapticService
    {
        public void OnInit()
        {
        }

        public bool IsActive { get; set; }

        public void SetHaptic(bool value)
        {
            IsActive = value;
        }

        public void HapticFeedback(HapticTypes type)
        {
            Haptics.Feedback(type);
        }
    }
}