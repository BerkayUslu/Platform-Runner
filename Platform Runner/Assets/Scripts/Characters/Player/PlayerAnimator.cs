using System;
using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Player;
using UnityEngine;

namespace PlatformRunner.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private int _currentState;

        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Running = Animator.StringToHash("Running");
        private readonly int FlyingBackDeath = Animator.StringToHash("FlyingBackDeath");

        private void Start()
        {
            ChangeAnimationState(Idle);
        }

        public void PlayAnimationForPlayerState(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Idle:
                    ChangeAnimationState(Idle);
                    break;
                case PlayerState.Moving:
                    ChangeAnimationState(Running);
                    break;
                case PlayerState.Dead:
                    ChangeAnimationState(FlyingBackDeath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, $"No implementation of {state} is found");
            }
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