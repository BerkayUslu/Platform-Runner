using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PlatformRunner
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        private IHealth _characterHealth;
        private IMovementAI _movementController;


        private void Start()
        {
            if (!TryGetComponent(out _movementController))
            {
                Debug.LogWarning("Animator could not find ai movement controller");
                Destroy(gameObject);
                return;
            }

            if (!TryGetComponent(out _characterHealth))
            {
                Debug.LogWarning("Enemy controller could not find health");
                Destroy(gameObject);
                return;
            }

            _characterHealth.Died += () =>
            {
                _capsuleCollider.isTrigger = false;
                _navMeshAgent.enabled = false;
            };

            _movementController.MoveToPosition(FindObjectOfType<DestinationObject>().GetPosition());
        }


    }
}