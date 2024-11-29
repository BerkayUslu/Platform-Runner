using UnityEngine;
using UnityEngine.AI;
using System;

namespace PlatformRunner
{
    public class EnemyMovementController : MonoBehaviour, IMovementController, IMovementAI
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private bool _canMove = true;
        private bool _isMoving = false;

        public event Action Moved;
        public event Action Stopped;

        private void FixedUpdate()
        {
            if (!_isMoving || !_navMeshAgent.enabled)
                return;

            if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.velocity = Vector3.zero;
                DisableMovement();
            }
        }

        public void DisableMovement()
        {
            _canMove = false;
            _isMoving = false;
            Stopped?.Invoke();
        }

        public void EnableMovement()
        {
            _canMove = true;
        }

        public void MoveToPosition(Vector3 position)
        {
            if (!_canMove) return;
                
            _navMeshAgent.SetDestination(position);
            _isMoving = true;
            Moved?.Invoke();
        }
    }
}