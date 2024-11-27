using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private IMovementController _movementController;
        private IHealth _health;
        public int _currentState;
        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Running = Animator.StringToHash("Running");
        private readonly int FlyingBackDeath = Animator.StringToHash("FlyingBackDeath");
        private readonly int EnemyDance = Animator.StringToHash("EnemyDance");


        private void Start()
        {
            if (!TryGetComponent(out _movementController))
            {
                Debug.LogWarning("Animator could not find ai movement controller");
                Destroy(gameObject);
                return;
            }

            if (!TryGetComponent(out _health))
            {
                Debug.LogWarning("Animator could not find health");
                Destroy(gameObject);
                return;
            }

            _movementController.Stopped += () => ChangeAnimationState(EnemyDance);
            _movementController.Moved += () => ChangeAnimationState(Running);
            _health.Died += () => ChangeAnimationState(FlyingBackDeath);

            ChangeAnimationState(Idle);
        }

        private void OnDisable()
        {

            _movementController.Stopped -= () => ChangeAnimationState(Idle);
            _movementController.Moved -= () => ChangeAnimationState(Running);
            _health.Died -= () => ChangeAnimationState(FlyingBackDeath);
        }

        private void ChangeAnimationState(int stateHash)
        {
            if (_currentState == stateHash)
                return;
            _animator.CrossFade(stateHash, 0);
            _currentState = stateHash;
        }
    }
}