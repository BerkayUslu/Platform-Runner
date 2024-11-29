// Script: EnemyController.cs
using UnityEngine;
using System;
using UnityEngine.AI;
using System.Collections;

namespace PlatformRunner
{
    public class EnemyController : MonoBehaviour, IHealth
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private Vector3 _initialPosition;
        private Transform _transform;
        private Vector3 _targetPosition;
        private bool _isDead;

        public bool IsDead => _isDead;
        public event Action Died;

        private void Start()
        {
            _transform = transform;
            _initialPosition = _transform.position;

            _movementController.Moved += () => _enemyAnimator.PlayRunAnimation();
            _movementController.Stopped += () => _enemyAnimator.PlayDanceAnimation();
        }

        private void OnDestroy()
        {
            _movementController.Moved -= () => _enemyAnimator.PlayRunAnimation();
            _movementController.Stopped -= () => _enemyAnimator.PlayDanceAnimation();
        }

        public void InitializeRunningTowardsTarget(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _movementController.MoveToPosition(targetPosition);
        }

        public void KillCharacter()
        {
            if (_isDead) return;

            _navMeshAgent.enabled = false;
            _isDead = true;
            _movementController.DisableMovement();
            _enemyAnimator.PlayDeathAnimation();
            Died?.Invoke();

            StartCoroutine(RestartEnemyAfterDeath(1));
        }

        private IEnumerator RestartEnemyAfterDeath(float delay)
        {
            yield return new WaitForSeconds(delay);
            ResetEnemy();
        }

        private void ResetEnemy()
        {
            _isDead = false;
            _transform.position = _initialPosition;
            _navMeshAgent.enabled = true;
            //_enemyAnimator.PlayIdleAnimation();
            _movementController.EnableMovement();
            _movementController.MoveToPosition(_targetPosition);
        }

    }
}