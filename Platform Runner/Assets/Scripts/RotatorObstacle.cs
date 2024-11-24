using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class RotatorObstacle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _bounceForce = 10;
        [SerializeField] private float _halfTurnTime;

        private float _halfTurn = 180;
        private Transform _transform;

        private Vector3 pos1;
        private Vector3 pos2;
        private Vector3 pos3;


        private void Start()
        {
            _transform = transform;
            RotatePlatform();
        }
        private void RotatePlatform()
        {
            _transform.DORotate(Vector3.up * _halfTurn, _halfTurnTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        }

        public void TriggerOfRotatingStick(Collider collider, Transform pointOnStick) => TriggerOccuredWithRotatingStick(collider, pointOnStick);
        public void TriggerOfRotator(Collider collider) => TouchOccuredWithRotator(collider);
        public void CollisionOfRotator(Collision collision)
        {
            TouchOccuredWithRotator(collision.collider);
        }

        private void TouchOccuredWithRotator(Collider collider)
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