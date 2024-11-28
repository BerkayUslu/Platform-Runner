using System;
using System.Collections;
using UnityEngine;

namespace PlatformRunner
{
    public class GameManager : Singleton<GameManager>
    {
        private GameState _state;
        public GameState State => _state;

        public static event Action<GameState> GameStateChanged;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(InitialStartWithDelay(0.1f));
        }

        private IEnumerator InitialStartWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            ChangeGameState(GameState.StartMenu);
        }

        public void ChangeGameState(GameState state)
        {
            _state = state;

            switch (state)
            {
                case GameState.StartMenu:
                    HandleStartMenuState();
                    break;
                case GameState.RunningGame:
                    HandleRunningGameState();
                    break;
                case GameState.WallPainting:
                    HandleWallPaintingState();
                    break;
                case GameState.Restart:
                    HandleRestartState();
                    break;
                case GameState.RunningGameFinish:
                    HandleRunningGameFinishState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, $"No implementation of {state} is found");
            }

            GameStateChanged?.Invoke(state);
        }

        private void HandleStartMenuState()
        {
            // Initialize start menu state
        }

        private void HandleRunningGameState()
        {
            // Initialize running game state
        }

        private void HandleWallPaintingState()
        {
            // Initialize wall painting state
        }

        private void HandleRestartState()
        {
            // Handle restart logic
        }

        private void HandleRunningGameFinishState()
        {
            // Handle finish state logic
        }

        public void StartGame()
        {
            ChangeGameState(GameState.RunningGame);
        }
    }

    [Serializable]
    public enum GameState
    {
        StartMenu,
        RunningGame,
        RunningGameFinish,
        WallPainting,
        Restart
    }
}