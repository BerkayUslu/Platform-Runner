using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Joystick _joystick;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private const float MOVEMENT_THRESHOLD = 0.01f;
        private Transform _transform;
        private Vector2 _rawInput;
        private Vector2 _normalizedInput;
        private Vector3 _mappedInput;

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _rawInput = _joystick.Direction;

            if (_rawInput.sqrMagnitude < MOVEMENT_THRESHOLD)
                return;

            ProcessInput();

            Move(_mappedInput);
            SetLookRotation(_mappedInput);
        }

        private void ProcessInput()
        {
            _normalizedInput = _rawInput.normalized;
            _mappedInput = new Vector3(_normalizedInput.x, 0, _normalizedInput.y);
        }

        private void Move(Vector3 direction)
        {
            _transform.position += direction * _movementSpeed;
        }

        private void SetLookRotation(Vector3 direction)
        {
            Quaternion LookRotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, LookRotation, _rotationSpeed);
        }
    }
}