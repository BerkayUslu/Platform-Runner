using DG.Tweening;
using PlatformRunner.Player;
using PlatformRunner.UI;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class RaceEndState : IGameState
    {
        private readonly Transform _paintingPosition;
        private readonly ITweenMovement _tweenMovement;
        private readonly RunningRaceManager _runningRaceManager;
        private readonly UiManager _uiManager;
        private readonly RaceEndPositionAnimation _raceEndPositionAnimation;

        public RaceEndState(ITweenMovement tweenMovement, Transform paintingPosition, RunningRaceManager runningRaceManager, UiManager uiManager, RaceEndPositionAnimation raceEndPositionAnimation)
        {
            _paintingPosition = paintingPosition;
            _tweenMovement = tweenMovement;
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
            _tweenMovement.MoveToPosition(position, 6)
                .OnComplete(() =>
                {
                    _runningRaceManager.DisableObstaclesAndEnemies();
                    GameManager.Instance.ChangeState<PaintingState>();
                })
                .SetEase(Ease.Linear);
        }
    }
}