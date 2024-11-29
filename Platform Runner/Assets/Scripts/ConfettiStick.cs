using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class ConfettiStick : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }


        public void PlayParticleEffect()
        {
            _particleSystem.Play();
        }
    }
}