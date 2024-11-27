using PlatformRunner.Player;
using PlatformRunner.UI;

namespace PlatformRunner.Core.StateMachine
{
    public class RunningState : IGameState
    {
        private readonly UiManager _uiManager;
        private readonly PlayerController _player;
        private readonly EnemyUnitsManager _enemyUnits;

        public RunningState(UiManager uiManager, PlayerController player, EnemyUnitsManager enemyUnits)
        {
            _uiManager = uiManager;
            _player = player;
            _enemyUnits = enemyUnits;
        }

        public void Enter()
        {
            _uiManager.ShowInGame();
            _enemyUnits.InitiateTheEnemeyUnits();
        }

        public void Exit()
        {
            _uiManager.HideAll();
            _player.StopMovement();
        }

        public void Update() { }
    }
}