using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformRunner.Core;
using PlatformRunner.Core.StateMachine;

namespace PlatformRunner
{
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.Player))
            {
                GameManager.Instance.ChangeState<RaceEndState>();
            }
        }
    }
}