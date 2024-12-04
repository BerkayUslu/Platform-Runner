using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlatformRunner
{
    public class MoveObjectBetweenPoints : MonoBehaviour
    {
        [SerializeField] private bool _isLocalMovement = false;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private float _movementTimeStartToEnd;
        [SerializeField] private float _movementTimeEndToStart;
        [SerializeField] private Ease _movementEaseStartToEnd;
        [SerializeField] private Ease _movementEaseEndToStart;

        private Transform _transform;
        private Sequence _movementSequence;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            MoveBetweenPoints();
        }

        private void OnDisable()
        {
            _movementSequence?.Kill();
        }

        private void MoveBetweenPoints()
        {
            _movementSequence = DOTween.Sequence();
            if (_isLocalMovement)
            {
                CreateLocalMovementSequence();
            }
            else
            {
                CreateMovementSequence();
            }
        }

        private void CreateMovementSequence()
        {
            _transform.position = _startPosition;
            _movementSequence.Append(_transform.DOMove(_endPosition, _movementTimeStartToEnd)
                    .SetEase(_movementEaseStartToEnd))
                .Append(_transform.DOMove(_startPosition, _movementTimeEndToStart)
                    .SetEase(_movementEaseEndToStart))
                .SetLoops(-1, LoopType.Restart);
        }

        private void CreateLocalMovementSequence()
        {
            _transform.localPosition = _startPosition;
            _movementSequence.Append(_transform.DOLocalMove(_endPosition, _movementTimeStartToEnd)
                    .SetEase(_movementEaseStartToEnd))
                .Append(_transform.DOLocalMove(_startPosition, _movementTimeEndToStart)
                    .SetEase(_movementEaseEndToStart))
                .SetLoops(-1, LoopType.Restart);
        }

        public void RestartMovement()
        {
            _movementSequence?.Kill();
            MoveBetweenPoints();
        }
    }
}