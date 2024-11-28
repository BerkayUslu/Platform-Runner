using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class EnemyController : MonoBehaviour, IHealth
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CapsuleCollider _capsuleCollider;

        private Vector3 _initialPosition;
        private IMovementAI _movementController;
        private bool _isDead = false;
        public event Action Died;
        private Transform _transform;
        private Vector3 _targetPosition;

        public bool IsDead => throw new NotImplementedException();

        private void Start()
        {
            if (!TryGetComponent(out _movementController))
            {
                Debug.LogWarning("Enemy controller could not find ai movement controller");
                Destroy(gameObject);
                return;
            }

            _transform = transform;
            _initialPosition = _transform.position;
        }

        public void InitializeRunningTowardsTarget(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _movementController.MoveToPosition(targetPosition);
        }

        public void KillCharacter()
        {
            _capsuleCollider.isTrigger = false;
            _navMeshAgent.enabled = false;
            if (!_isDead)
                Died?.Invoke();
            _isDead = true;

            StartCoroutine(RestartEnemyAfterDeath(1));
        }

        private IEnumerator RestartEnemyAfterDeath(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isDead = false;
            _transform.position = _initialPosition;
            _capsuleCollider.isTrigger = true;
            _navMeshAgent.enabled = true;

            _movementController.MoveToPosition(_targetPosition);
        }
    }
}