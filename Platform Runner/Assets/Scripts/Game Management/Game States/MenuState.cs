using PlatformRunner.UI;

namespace PlatformRunner.Core.StateMachine
{
    public class MenuState : IGameState
    {
        private readonly UiManager _uiManager;

        public MenuState(UiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Enter()
        {
            _uiManager.ShowMenu();
        }

        public void Exit()
        {
            _uiManager.HideAll();
        }

        public void Update() { }
    }
}