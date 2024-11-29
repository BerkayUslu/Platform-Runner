using DG.Tweening;
using PlatformRunner.Player;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class RaceEndState : IGameState
    {
        private readonly Transform _paintingPosition;
        private readonly ITweenMovement _tweenMovement;
        private readonly RunningRaceManager _runningRaceManager;

        public RaceEndState(ITweenMovement tweenMovement, Transform paintingPosition, RunningRaceManager runningRaceManager)
        {
            _paintingPosition = paintingPosition;
            _tweenMovement = tweenMovement;
            _runningRaceManager = runningRaceManager;
        }

        public void Enter()
        {
            MovePlayerToPaintingPosition(_paintingPosition.position);
        }

        public void Exit() { }

        public void Update() { }

        public void MovePlayerToPaintingPosition(Vector3 position)
        {
            Debug.Log("Animate UI player position");
            _tweenMovement.MoveToPosition(position, 6)
                .OnComplete(() => GameManager.Instance.ChangeState<PaintingState>())
                .SetEase(Ease.Linear);
        }
    }
}