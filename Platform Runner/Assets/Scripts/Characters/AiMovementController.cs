using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class AiMovementController : MonoBehaviour, IMovementController, IMovementAI
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public event Action Moved;
        public event Action Stopped;

private bool _canMove = true;
        private bool _isMoving = false;

        private void FixedUpdate()
        {
            if (!_isMoving || !_navMeshAgent.enabled)
                return;

            if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.velocity = Vector3.zero;
                DisableMovement();
                return;
            }
        }

        public void DisableMovement()
        {
            _canMove = false;
            _isMoving = false;
            Stopped?.Invoke();
        }

        public void MoveToPosition(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
            _isMoving = true;
            Moved?.Invoke();
        }

        public void EnableMovement()
        {
            _canMove = true;
        }
    }
}