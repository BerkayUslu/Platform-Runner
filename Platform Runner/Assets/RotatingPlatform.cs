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


        private Transform _transform;
        private float _halfTurn = 180;
        private float _angularSpeed;

        private void Awake()
        {
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

                collisionInfo.rigidbody.AddForce(Vector3.left * force, ForceMode.VelocityChange);
            }
        }
    }
}