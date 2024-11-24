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

        public static void DrawRayWithArrow(Vector3 start, Vector3 direction, Color color, float duration = 0.0f)
        {
            // Draw the main ray
            Debug.DrawRay(start, direction, color, duration);

            // Calculate the end point of the ray
            Vector3 end = start + direction;

            // Make arrow head size proportional to the direction length
            float arrowLength = direction.magnitude * 0.2f; // 20% of the ray length
            float arrowAngle = 20.0f;

            // Calculate the arrow head points
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowAngle, 0) * Vector3.forward;

            // Draw the arrow head lines
            Debug.DrawRay(end, right * arrowLength, color, duration);
            Debug.DrawRay(end, left * arrowLength, color, duration);
        }
    }
}