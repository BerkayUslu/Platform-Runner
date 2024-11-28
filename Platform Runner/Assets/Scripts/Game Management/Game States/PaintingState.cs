using PlatformRunner.UI;

namespace PlatformRunner.Core.StateMachine
{
    public class PaintingState : IGameState
    {
        private readonly UiManager _uiManager;
        private readonly CameraManager _cameraManager;
        private readonly PaintingManager _paintingManager;

        public PaintingState(UiManager uiManager, CameraManager cameraManager, PaintingManager paintingManager)
        {
            _uiManager = uiManager;
            _cameraManager = cameraManager;
            _paintingManager = paintingManager;
        }

        public void Enter()
        {
            _paintingManager.EnablePainting();
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