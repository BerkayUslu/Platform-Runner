using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.Player))
            {
                GameManager.Instance.ChangeGameState(GameState.RunningGameFinish);
            }
        }
    }
}