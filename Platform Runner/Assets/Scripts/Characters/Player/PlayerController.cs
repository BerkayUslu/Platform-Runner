using System;
using System.Collections;
using UnityEngine;
using PlatformRunner.Core;

namespace PlatformRunner.Player
{
    public class PlayerController : MonoBehaviour, IHealth
    {
        public static PlayerController Instance;
        public event Action Died;
        public bool IsDead { get; private set; }

        [Header("References")]
        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private float _respawnDelay = 1.5f;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            _movementController.Moved += () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Moving);
            _movementController.Stopped += () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Idle);
        }

        private void OnDestroy()
        {
            _movementController.Moved -= () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Moving);
            _movementController.Stopped -= () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Idle);
        }

        public void KillCharacter()
        {
            if (IsDead) return;

            IsDead = true;
            _movementController.StopMovement();
            _playerAnimator.PlayAnimationForPlayerState(PlayerState.Dead);
            PlayerStatsManager.Instance.IncreaseFail();
            StartCoroutine(nameof(RespawnAfterDelay));
            Died?.Invoke();
        }

        private IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            GameManager.Instance.RestartCurrentScene();
        }
    }
}