using PlatformRunner.UI;

namespace PlatformRunner.Core.StateMachine
{
    public class PaintingState : IGameState
    {
        private readonly UiManager _uiManager;
        private readonly CameraManager _cameraManager;

        public PaintingState(UiManager uiManager, CameraManager cameraManager)
        {
            _uiManager = uiManager;
            _cameraManager = cameraManager;
        }

        public void Enter()
        {
            _uiManager.ShowPainting();
            _cameraManager.DeactivateGameCamera();
        }

        public void Exit()
        {
            _uiManager.HideAll();
        }

        public void Update() { }
    }
}