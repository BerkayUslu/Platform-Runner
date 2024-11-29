using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class ConfettiSticksController : MonoBehaviour
    {
        [SerializeField] private ConfettiStick[] _confettiSticks;

        public void PlayConfettiParticles(float stopTime)
        {
            foreach (ConfettiStick confettiStick in _confettiSticks)
                confettiStick.PlayParticleEffect();

                Invoke("StopConfettiParticles", stopTime);
        }

        public void StopConfettiParticles()
        {
            foreach (ConfettiStick confettiStick in _confettiSticks)
                confettiStick.StopParticleEffect();
        }

    }
}