using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class ConfettiSticksController : MonoBehaviour
    {
        [SerializeField] private ConfettiStick[] _confettiSticks;

        public void PlayConfettiParticles()
        {
            foreach (ConfettiStick confettiStick in _confettiSticks)
                confettiStick.PlayParticleEffect();
        }
    }
}