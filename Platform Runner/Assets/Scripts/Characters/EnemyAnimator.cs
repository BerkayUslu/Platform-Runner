using UnityEngine;

namespace PlatformRunner
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private int _currentState;
        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Running = Animator.StringToHash("Running");
        private readonly int FlyingBackDeath = Animator.StringToHash("FlyingBackDeath");
        private readonly int Dance = Animator.StringToHash("Dance");

        private void Start()
        {
            ChangeAnimationState(Idle);
        }

        public void PlayIdleAnimation() => ChangeAnimationState(Idle);
        public void PlayRunAnimation() => ChangeAnimationState(Running);
        public void PlayDeathAnimation() => ChangeAnimationState(FlyingBackDeath);
        public void PlayDanceAnimation() => ChangeAnimationState(Dance);

        private void ChangeAnimationState(int stateHash)
        {
            if (_currentState == stateHash) return;
            
            _animator.CrossFade(stateHash, 0);
            _currentState = stateHash;
        }
    }
}