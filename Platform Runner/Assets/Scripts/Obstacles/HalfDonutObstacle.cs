using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class HalfDonutObstacle : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField] private float _bounceForce = 10;
        [SerializeField] private float _thrustLocalPositionX = -0.12f;
        [SerializeField] private float _drawBackLocalPositionX = 0.15f;
        [SerializeField] private float _thrustTime;
        [SerializeField] private float _drawBackTime;

        [Header("References")]
        [SerializeField] private Transform _movingStick;

        private Sequence _stickAnimationSequence;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            AnimateStickMovement();
        }

        private void AnimateStickMovement()
        {
            _movingStick.localPosition = _drawBackLocalPositionX * Vector3.right;

            _stickAnimationSequence = DOTween.Sequence();
            _stickAnimationSequence.Append(
                _movingStick.DOLocalMoveX(_thrustLocalPositionX, _thrustTime).SetEase(Ease.InExpo));
            _stickAnimationSequence.Append(
                _movingStick.DOLocalMoveX(_drawBackLocalPositionX, _drawBackTime).SetEase(Ease.OutSine));

            _stickAnimationSequence.SetLoops(-1, LoopType.Restart);

            _stickAnimationSequence.Play();
        }

        public void CollisionOccuredWithDonut(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                BounceObjectBack(collider.GetComponent<Rigidbody>(), collider.transform.position - _transform.position, _bounceForce);
            }
        }

        private void BounceObjectBack(Rigidbody rigidbody, Vector3 direction, float force)
        {
            direction -= direction.y * Vector3.up;
            direction = direction.normalized;
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

    }

}
