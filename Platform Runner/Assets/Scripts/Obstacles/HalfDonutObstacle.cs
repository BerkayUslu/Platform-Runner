using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class HalfDonutObstacle : ObstacleBase
    {
        [Header("Settings")] [SerializeField] private float _bounceForce = 10;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void TriggerOfHalfDonutPart(Collider other)
        {
            TryKillCollidedObject(other);

            if (!IsEnemOrPlayer(other)) return;

            BounceObjectBack(other.GetComponent<Rigidbody>(), other.transform.position - _transform.position,
                _bounceForce);
        }

        public void TriggerOfStickPart(Collider other)
        {
            TryKillCollidedObject(other);
        }

        private void BounceObjectBack(Rigidbody rb, Vector3 direction, float force)
        {
            direction -= direction.y * Vector3.up;
            direction = direction.normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}