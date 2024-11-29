using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace PlatformRunner
{
    public class RotatingPlatform : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _halfTurnTime;
        [SerializeField] private float _forceModifier;
        [SerializeField] private float _velocityModifier;
        [SerializeField] private bool _turnReverse;


        private Transform _transform;
        private float _halfTurn = 180;
        private float _angularSpeed;

        private void Awake()
        {
            if (_turnReverse)
                _halfTurn = -_halfTurn;
            _transform = transform;
            _angularSpeed = Mathf.Abs(_halfTurn) / _halfTurnTime;
        }

        private void Start()
        {
            RotatePlatform();
        }

        [ContextMenu("Rotate")]
        private void RotatePlatform()
        {
            _transform.DORotate(Vector3.forward * _halfTurn, _halfTurnTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        }

        [ContextMenu("Reset")]
        private void ResetPlatform()
        {
            _transform.rotation = Quaternion.identity;
        }

        public void OnCollisionStay(Collision collisionInfo)
        {
            if (collisionInfo.gameObject.CompareTag("Player"))
            {
                float force = _forceModifier * _angularSpeed * Time.deltaTime;
                var normal = collisionInfo.GetContact(0).normal;
                Vector3 direction = Vector3.Cross(normal, Vector3.forward).normalized;
                if (_turnReverse)
                    direction = -direction;
                collisionInfo.rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
                collisionInfo.rigidbody.velocity = direction * _velocityModifier;
            }
        }
    }
}