using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Player;
using UnityEngine;

namespace PlatformRunner.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerHealth _health;

        private int _currentState;
        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Running = Animator.StringToHash("Running");
        private readonly int FlipBackDeath = Animator.StringToHash("FlipBackDeath");
        private readonly int FlyingBackDeath = Animator.StringToHash("FlyingBackDeath");


        private void Start()
        {
            _movementController.Stopped += () => ChangeAnimationState(Idle);
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