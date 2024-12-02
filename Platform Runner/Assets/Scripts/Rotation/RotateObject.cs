using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace PlatformRunner
{
    public class RotateObject : MonoBehaviour, IRotationDirection
    {
        [Header("Y Axis Rotation")] [SerializeField]
        private bool _isRotatingAroundY;

        [SerializeField] private bool _isReverseRotationY;
        [SerializeField] private float _yAxisRotationAmount = 360;
        [SerializeField] private float _yAxisRotationTime;
        [SerializeField] private Ease _easeTypeY = Ease.Linear;

        [Header("Z Axis Rotation")] [SerializeField]
        private bool _isRotatingAroundZ;

        [SerializeField] private bool _isReverseRotationZ;
        [SerializeField] private float _zAxisRotationAmount = 360;
        [SerializeField] private float _zAxisRotationTime;
        [SerializeField] private Ease _easeTypeZ = Ease.Linear;

        private readonly List<Tween> _tweens = new List<Tween>();
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            if (_isRotatingAroundY)
                RotateAroundAxis(Vector3.up, _yAxisRotationAmount, _yAxisRotationTime, _easeTypeY, _isReverseRotationY);
            if (_isRotatingAroundZ)
                RotateAroundAxis(Vector3.forward, _zAxisRotationAmount, _zAxisRotationTime, _easeTypeZ,
                    _isReverseRotationZ);
        }

        private void OnDisable()
        {
            foreach (var tween in _tweens)
            {
                tween?.Kill();
            }
        }

        private void RotateAroundAxis(Vector3 axis, float rotationAmount, float rotationTime, Ease easeType,
            bool reverse)
        {
            axis = axis.normalized;

            rotationAmount = reverse ? -rotationAmount : rotationAmount;

            var endValue = rotationAmount * axis;

            var tween = _transform.DORotate(endValue, rotationTime, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(easeType);

            _tweens.Add(tween);
        }

        public Vector3 GetDirectionAxisY()
        {
            if (!_isRotatingAroundY) return Vector3.zero;

            return _isReverseRotationY ? Vector3.down : Vector3.up;
        }

        public Vector3 GetDirectionAxisZ()
        {
            if (!_isRotatingAroundZ) return Vector3.zero;

            return _isReverseRotationZ ? Vector3.back : Vector3.forward;
        }
    }
}