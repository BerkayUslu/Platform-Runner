using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class ShiningObstacle : ObstacleBase
    {
        [SerializeField] private float _targetXPositionOne;
        [SerializeField] private float _targetXPositionTwo;
        [SerializeField] private float _moveTime;

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
        }


        private void MoveBetweenTargets()
        {
            _leftRightMovementSequence = DOTween.Sequence();
            _transform.SetX(_targetXPositionTwo);

            _leftRightMovementSequence
                .Append(_transform.DOMoveX(_targetXPositionOne, _moveTime));
            _leftRightMovementSequence
                .Append(_transform.DOMoveX(_targetXPositionTwo, _moveTime));

            _leftRightMovementSequence.SetLoops(-1);
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryKillCollidedObject(collision.collider);
        }
        
    }
}