using System;
using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Core;
using UnityEngine;

namespace PlatformRunner.Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        public event Action<PlayerState> StateChanged;

        private PlayerState _currentState;
        public PlayerState CurrentState { get => _currentState; }

        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerAnimator _playerAnimator;

        [SerializeField] private float _respawnDelay = 1.5f;


        private void Start()
        {
            _health.Died += () => ChangeState(PlayerState.Dead);
            _movementController.Moved += () => ChangeState(PlayerState.Moving);
            _movementController.Stopped += () => ChangeState(PlayerState.Idle);
        }

        private void OnDestroy()
        {
            _health.Died -= () => ChangeState(PlayerState.Dead);
            _movementController.Moved -= () => ChangeState(PlayerState.Moving);
            _movementController.Stopped -= () => ChangeState(PlayerState.Idle);
        }

        public void ChangeState(PlayerState state)
        {
            _currentState = state;
            switch (state)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Moving:
                    break;
                case PlayerState.Dead:
                    _movementController.StopMovement();
                    PlayerStatsManager.Instance.IncreaseFail();
                    StartCoroutine(nameof(RespawnAfterDelay));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, $"No implementation of {state} is found");
            }

            _playerAnimator.PlayAnimationForPlayerState(state);
            StateChanged?.Invoke(state);
        }

        private IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            GameManager.Instance.RestartCurrentScene();
        }
    }
}