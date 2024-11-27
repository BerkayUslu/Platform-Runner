using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformRunner
{
    public class PlayerController : MonoBehaviour, IHealth
    {
        private IMovementController _movementController;
        [SerializeField] private Transform _paintingPosition;
        private bool _isDead = false;
        public bool IsDead { get { return _isDead; } }
        public event Action Died;

        private void Start()
        {
            if (!TryGetComponent(out _movementController))
            {
                Debug.LogWarning("PlayerController could not find movement controller");
                Destroy(gameObject);
                return;
            }

            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        public void KillCharacter()
        {
            if (!_isDead)
                Died?.Invoke();

            _movementController.StopMovement();
            _isDead = true;

            PlayerStatsManager.Instance.IncreaseFail();
            StartCoroutine(RestartGameAfterDeath(1.5f));
        }

        private IEnumerator RestartGameAfterDeath(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnGameStateChanged(GameState state)
        {

            if (state == GameState.RunningGameFinish)
            {
                _movementController.StopMovement();

                if (_movementController is ITweenMovement tweenMovement)
                {
                    tweenMovement.MoveToPosition(_paintingPosition.position, 6)
                        .OnKill(() =>
                        {
                            GameManager.Instance.ChangeGameState(GameState.WallPainting);
                        }).SetEase(Ease.Linear);
                }
            }

            if (state == GameState.WallPainting)
                _movementController.StopMovement();

        }
    }
}