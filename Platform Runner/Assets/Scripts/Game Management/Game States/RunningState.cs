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

        public RunningState(UiManager uiManager, EnemyUnitsManager enemyUnits, IMovementController playerMovement)
        {
            _uiManager = uiManager;
            _enemyUnits = enemyUnits;
            _playerMovement = playerMovement;
        }

        public void Enter()
        {
            _uiManager.ShowInGame();
            _enemyUnits.InitiateTheEnemeyUnits();
        }

        public void Exit()
        {
            _playerMovement.StopMovement();
            _uiManager.HideAll();
        }

        public void Update() { }
    }
}