using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner.Player
{
    public class PlayerMovementController : MonoBehaviour, IMovementController
    {
        [Header("References")]
        [SerializeField] private Joystick _joystick;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

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
            _transform = transform;
        }

        private void FixedUpdate()
        {
            if (!_canMove || !_joystick)
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

        public void DisableMovement()
        {
            _canMove = false;
        }

        public void EnableMovement()
        {
            _canMove = true;
        }
        
        public Tween MoveToPosition(Vector3 position, float time)
        {
            return _transform.DOMove(new Vector3(position.x, 0, position.z), time).OnKill(() =>
            {
                Stopped?.Invoke();
            });
        }
    }
}