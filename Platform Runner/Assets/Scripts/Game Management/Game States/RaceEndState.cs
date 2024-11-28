using DG.Tweening;
using PlatformRunner.Player;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class RaceEndState : IGameState
    {
        private readonly Transform _paintingPosition;
        private readonly ITweenMovement _tweenMovement;

        public RaceEndState( ITweenMovement tweenMovement, Transform paintingPosition)
        {
            _paintingPosition = paintingPosition;
            _tweenMovement = tweenMovement;
        }

        public void Enter()
        {
            MovePlayerToPaintingPosition(_paintingPosition.position);
        }

        public void Exit() { }

        public void Update() { }

        public void MovePlayerToPaintingPosition(Vector3 position)
        {
            _tweenMovement.MoveToPosition(position, 6)
                .OnKill(() => GameManager.Instance.ChangeState<PaintingState>())
                .SetEase(Ease.Linear);
        }
    }
}