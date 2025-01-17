using DG.Tweening;
using PlatformRunner.Player;
using PlatformRunner.UI;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class RunningState : IGameState
    {
        private readonly UiManager _uiManager;
        private readonly IMovementController _playerMovement;
        private readonly EnemyUnitsManager _enemyUnits;
        private readonly StartCountdownAnimation _startCountdown;

        public RunningState(UiManager uiManager, EnemyUnitsManager enemyUnits, IMovementController playerMovement, StartCountdownAnimation startCountdown)
        {
            _uiManager = uiManager;
            _enemyUnits = enemyUnits;
            _playerMovement = playerMovement;
            _startCountdown = startCountdown;
        }

        public void Enter()
        {
            _playerMovement.DisableMovement();
            _uiManager.ShowInGame();
            _uiManager.ShowAnimation();
            _startCountdown.AnimateCountdown(0.8f, 0.2f, 0.3f).OnComplete(() =>
            {
                _playerMovement.EnableMovement();
                _enemyUnits.InitiateTheEnemeyUnits();
            });
        }

        public void Exit()
        {
            _playerMovement.DisableMovement();
            _uiManager.HideAll();
        }

        public void Update() { }
    }
}