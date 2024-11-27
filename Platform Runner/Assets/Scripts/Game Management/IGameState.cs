namespace PlatformRunner.Core.StateMachine
{
    public interface IGameState
    {
        public void Enter();
        public void Exit();
        public void Update();
    }
}