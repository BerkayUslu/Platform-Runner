using System;
using System.Collections;
using UnityEngine;
using PlatformRunner.Core;
using TMPro;
using UnityEngine.Apple;

namespace PlatformRunner.Player
{
    public class PlayerController : MonoBehaviour, IHealth, IPlayerAnimate
    {
        public static PlayerController Instance;
        public event Action Died;
        public bool IsDead { get; private set; }
        private Transform _transform;
        private Rigidbody _rigidbody;
        private int _frameCounter = 0;
        private float _initialDrag;

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
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _initialDrag = _rigidbody.drag;
            _movementController.Moved += () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Moving);
            _movementController.Stopped += () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Idle);
        }

        private void OnDestroy()
        {
            _movementController.Moved -= () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Moving);
            _movementController.Stopped -= () => _playerAnimator.PlayAnimationForPlayerState(PlayerState.Idle);
        }

        private void Update()
        {
            _frameCounter++;
            if (_frameCounter == 10)
            {
                _frameCounter = 0;
                ChangeDragIfPlayerFallen();
            }
        }

        private void ChangeDragIfPlayerFallen()
        {
            if (!CheckGround())
            {
                _rigidbody.drag = 0;
            }
        }

        public void KillCharacter()
        {
            if (IsDead) return;

            IsDead = true;
            _movementController.DisableMovement();
            _playerAnimator.PlayAnimationForPlayerState(PlayerState.Dead);
            PlayerStatsManager.Instance.IncreaseFail();
            StartCoroutine(nameof(RespawnAfterDelay));
            Died?.Invoke();
        }

        public void PlayCelebrateAnimation()
        {
            _playerAnimator.PlayCelebrationAnimation();
        }

        private bool CheckGround()
        {
            return Physics.Raycast(_transform.position + Vector3.up * 0.5f, -_transform.up, 1f, LayerMask.GetMask("Environment"));
        }

        private IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            GameManager.Instance.RestartCurrentScene();
        }
    }
}