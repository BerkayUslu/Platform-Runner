using UnityEngine;
using DG.Tweening;
using PlatformRunner.Core;
using PlatformRunner.Player;
using PlatformRunner.Core.StateMachine;

namespace PlatformRunner.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMovementController _movementController;

        private void Start()
        {
            if (!TryGetComponent(out _movementController))
            {
                Debug.LogWarning("PlayerController could not find movement controller");
                Destroy(gameObject);
                return;
            }

            _health.Died += OnPlayerDeath;
        }

        private void OnDestroy()
        {
            _health.Died -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _movementController.StopMovement();
        }

        public void StopMovement()
        {
            _movementController.StopMovement();
        }

        public void MoveToPaintingPosition(Vector3 position)
        {
            if (_movementController is ITweenMovement tweenMovement)
            {
                tweenMovement.MoveToPosition(position, 6)
                    .OnKill(() => GameManager.Instance.ChangeState<PaintingState>())
                    .SetEase(Ease.Linear);
            }
        }
    }
}