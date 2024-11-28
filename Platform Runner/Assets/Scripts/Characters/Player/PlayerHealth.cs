using UnityEngine;
using System;
using PlatformRunner.Core;
using System.Collections;

namespace PlatformRunner.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public event Action Died;
        public bool IsDead { get; private set; }

        private void OnEnable()
        {
            IsDead = false;
        }

        public void KillCharacter()
        {
            if (IsDead) return;

            IsDead = true;
            Died?.Invoke();
        }
    }
}