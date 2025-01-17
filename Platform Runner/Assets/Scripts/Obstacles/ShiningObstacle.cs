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

        [SerializeField] private float _halfTurnTime;
        [SerializeField] private bool _spinReverse;
        private float _halfTurn = 180;


        private Transform _transform;
        private Sequence _leftRightMovementSequence;
        private ParticleColorController _particleColorController;

        private void Awake()
        {
            _transform = transform;
            _particleColorController = GetComponent<ParticleColorController>();

            if (_particleColorController == null)
            {
                Debug.LogWarning("ShiningObstacle: ParticleColorController component not found");
            }

            MoveBetweenTargets();
            RotateObstacle();
        }

        private void RotateObstacle()
        {
            _transform.DOLocalRotate(Vector3.up * _halfTurn, _halfTurnTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        }

        private void MoveBetweenTargets()
        {
            _leftRightMovementSequence = DOTween.Sequence();
            _transform.SetX( _targetXPositionTwo);

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
                _particleColorController.ChangeToRandomColor();

                IHealth characterHealth;
                if (collider.TryGetComponent(out characterHealth))
                {
                    characterHealth.KillCharacter();
                }
            }
        }
    }
}