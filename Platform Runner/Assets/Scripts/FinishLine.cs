using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformRunner.Core;
using PlatformRunner.Core.StateMachine;

namespace PlatformRunner
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private ConfettiSticksController _confettiSticksController;
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.Player))
            {
                OnPlayerFinished();
            }
        }

        private void OnPlayerFinished()
        {
            RunningRaceManager.Instance.PlayerPassedFinishLine();
            _confettiSticksController.PlayConfettiParticles();
        }
    }
}