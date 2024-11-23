using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class CharacterController : MonoBehaviour, IHealth
    {
        private bool _dead = false;
        public bool Dead => _dead;
        public event Action Died;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.DeadlyObstacle))
            {
                KillCharacter();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tags.DeadlyObstacle))
            {
                KillCharacter();
            }
        }

        private void KillCharacter()
        {
            if (!_dead)
                Died?.Invoke();
        }
    }
}