using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public interface IHealth
    {
        public bool IsDead { get; }
        public event Action Died;
        public void KillCharacter();
    }
}