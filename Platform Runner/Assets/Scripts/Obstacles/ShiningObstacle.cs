using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class ShiningObstacle : MonoBehaviour
    {
        [SerializeField] private float _targetXPositionOne;
        [SerializeField] private float _targetXPositionTwo;

        [SerializeField] private float _moveTime;

        private Transform _transform;
        private Sequence _leftRightMovementSequence;

        private void Awake()
        {
            _transform = transform;
            MoveBetweenTargets();
        }

        private void MoveBetweenTargets()
        {
            _leftRightMovementSequence = DOTween.Sequence();
            _transform.SetX(_transform.position.x + _targetXPositionTwo);

            _leftRightMovementSequence
                .Append(_transform.DOMoveX(_targetXPositionOne, _moveTime));
            _leftRightMovementSequence
                .Append(_transform.DOMoveX(_targetXPositionTwo, _moveTime));

            _leftRightMovementSequence.SetLoops(-1);
        }

        private void OnTriggerEnter(Collider collider)
        {
            OnTouch(collider);
        }

        private void OnCollisionEnter(Collision collision)
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