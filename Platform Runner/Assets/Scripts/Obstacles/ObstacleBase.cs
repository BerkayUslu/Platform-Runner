using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  PlatformRunner
{
    public class ObstacleBase : MonoBehaviour, IObstacle
    {
        private void KillCollidedObject(Collider other)
        {
            if (other.TryGetComponent(out IHealth health))
            {
                health.KillCharacter();
            }
        }
        
        protected bool IsEnemOrPlayer(Collider other) =>
            other.CompareTag(Tags.Player) || other.CompareTag(Tags.Enemy);

        public virtual void TryKillCollidedObject(Collider other)
        {
            KillCollidedObject(other);
        }
    }
}
