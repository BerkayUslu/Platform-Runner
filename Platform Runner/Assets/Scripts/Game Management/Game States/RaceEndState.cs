using DG.Tweening;
using PlatformRunner.Player;
using PlatformRunner.UI;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class RaceEndState : IGameState
    {
        private readonly Transform _paintingPosition;
        private readonly IMovementController _playerMovement;
        private readonly RunningRaceManager _runningRaceManager;
        private readonly UiManager _uiManager;
        private readonly RaceEndPositionAnimation _raceEndPositionAnimation;

        public RaceEndState(IMovementController playerMovement, Transform paintingPosition, RunningRaceManager runningRaceManager, UiManager uiManager, RaceEndPositionAnimation raceEndPositionAnimation)
        {
            _paintingPosition = paintingPosition;
            _playerMovement = playerMovement;
            _runningRaceManager = runningRaceManager;
            _uiManager = uiManager;
            _raceEndPositionAnimation = raceEndPositionAnimation;
        }

        public void Enter()
        {
            _uiManager.ShowAnimation();
            _raceEndPositionAnimation.AnimateFinalPosition(_runningRaceManager.PlayerFinalPosition, 3, 0.3f, 1);
            MovePlayerToPaintingPosition(_paintingPosition.position);
        }

        public void Exit()
        {
            _uiManager.HideAll();
        }

        public void Update() { }

        public void MovePlayerToPaintingPosition(Vector3 position)
        {
            _playerMovement.MoveToPosition(position, 6)
                .OnComplete(() =>
                {
                    _runningRaceManager.DisableObstaclesAndEnemies();
                    GameManager.Instance.ChangeState<PaintingState>();
                })
                .SetEase(Ease.Linear);
        }
    }
}