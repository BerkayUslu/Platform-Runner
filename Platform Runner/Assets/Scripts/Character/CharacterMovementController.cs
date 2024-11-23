using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class CharacterMovementController : MonoBehaviour, IMovementController
    {
        [Header("References")]
        [SerializeField] private Joystick _joystick;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private IHealth _health;
        private Rigidbody _rigidbody;
        private const float MOVEMENT_THRESHOLD = 0.1f;
        private Transform _transform;
        private Vector2 _rawInput;
        private Vector2 _normalizedInput;
        private Vector3 _mappedInput;
        private bool _wasCharacterMoving = false;
        private bool _canMove = true;

        public event Action Moved;
        public event Action Stopped;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void Start()
        {
            if (!TryGetComponent(out _health))
            {
                Debug.LogWarning("CharacterMovementController could not find health");
                Destroy(gameObject);
                return;
            }

            _health.Died += StopMovement;
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;

            _rawInput = _joystick.Direction;

            if (IsInputAboveTreshold(_rawInput))
            {
                _mappedInput = ProcessVector2Input(_rawInput);
                Move(_mappedInput);
                SetLookRotation(_mappedInput);

                if (!_wasCharacterMoving)
                    Moved?.Invoke();

                _wasCharacterMoving = true;
            }
            else
            {
                if (_wasCharacterMoving)
                    Stopped?.Invoke();

                _wasCharacterMoving = false;
            }
        }

        private bool IsInputAboveTreshold(Vector2 input)
        {
            return input.sqrMagnitude > MOVEMENT_THRESHOLD;
        }

        private Vector3 ProcessVector2Input(Vector2 input)
        {
            _normalizedInput = input.normalized;
            return new Vector3(_normalizedInput.x, 0, _normalizedInput.y);
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

        public void StopMovement()
        {
            _canMove = false;
        }
    }
}