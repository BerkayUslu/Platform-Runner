using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace PlatformRunner
{
    [RequireComponent(typeof(IRotationDirection))]
    public class RotatingPlatform : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _forceModifier;
        [SerializeField] private float _velocityModifier;

        private bool _isTurnReverse = false;

        private void Awake()
        {
            _isTurnReverse = GetComponent<IRotationDirection>().GetDirectionAxisZ() == Vector3.back;
        }

        public void OnCollisionStay(Collision collisionInfo)
        {
            if (!collisionInfo.gameObject.CompareTag("Player")) return;

            ApplyForceToPlayer(collisionInfo);
        }

        private void ApplyForceToPlayer(Collision collisionInfo)
        {
            float force = _forceModifier * Time.deltaTime;

            var normal = collisionInfo.GetContact(0).normal;

            Vector3 direction = Vector3.Cross(normal, Vector3.forward).normalized;

            if (_isTurnReverse)
                direction = -direction;

            collisionInfo.rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
            collisionInfo.rigidbody.velocity = direction * _velocityModifier;
        }
    }
}