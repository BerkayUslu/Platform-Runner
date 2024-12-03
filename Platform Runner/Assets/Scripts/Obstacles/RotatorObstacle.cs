using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class RotatorObstacle : ObstacleBase
    {
        [Header("Settings")] [SerializeField] private float _bounceForce = 10;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void TriggerOfRotatingStick(Collider collider, Transform pointOnStick) =>
            TriggerOccuredWithRotatingStick(collider, pointOnStick);

        public void CollisionOfRotator(Collision collision)
        {
            TryKillCollidedObject(collision.collider);
        }

        private void TriggerOccuredWithRotatingStick(Collider collider, Transform pointOnStick)
        {
            if (collider.gameObject.CompareTag(Tags.Player) || collider.gameObject.CompareTag(Tags.Enemy))
            {
                Vector3 colliderPosition = collider.transform.position;
                Vector3 rayDirection = GetCollisionForceDirection(colliderPosition, pointOnStick);
                BounceObjectBack(collider.GetComponent<Rigidbody>(), -rayDirection, _bounceForce);
            }
        }

        private Vector3 GetCollisionForceDirection(Vector3 colliderPosition, Transform stickTransform)
        {
            Vector3 directionFromCenter = (colliderPosition - stickTransform.position).normalized;
            Vector3 forceDirection = Vector3.Cross(stickTransform.up, directionFromCenter).normalized;
            if (Vector3.Dot(stickTransform.right, directionFromCenter) < 0)
            {
                forceDirection = -forceDirection;
            }

            return -forceDirection;
        }

        private void BounceObjectBack(Rigidbody rigidbody, Vector3 direction, float force)
        {
            direction -= direction.y * Vector3.up;
            direction = direction.normalized;
            rigidbody.velocity = direction;
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}