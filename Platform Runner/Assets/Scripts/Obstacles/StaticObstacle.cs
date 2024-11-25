using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class StaticObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            OnTouch(collider);
        }

        private void OnCollisiontEnter(Collision collision)
        {
            OnTouch(collision.collider);
        }

        private void OnTouch(Collider collider)
        {
            if (collider.gameObject.CompareTag(Tags.Player) || collider.gameObject.CompareTag(Tags.Enemy))
            {
                IHealth characterHealth;
                if (collider.TryGetComponent(out characterHealth))
                {
                    characterHealth.KillCharacter();
                }
            }
        }
    }
}