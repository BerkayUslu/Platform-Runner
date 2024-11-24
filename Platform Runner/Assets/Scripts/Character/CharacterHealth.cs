using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        private bool _isDead = false;
        public bool IsDead { get { return _isDead; } }
        public event Action Died;

        public void KillCharacter()
        {
            if (!_isDead)
                Died?.Invoke();
            _isDead = true;

        }
    }
}