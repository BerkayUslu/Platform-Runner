using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private GameState _state;
        public GameState State { get { return _state; } }

        public static event Action<GameState> GameStateChanged;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            StartCoroutine(InitialStartWithDelay(0.5f));
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
                    break;
                case GameState.RunningGame:
                    break;
                case GameState.WallPainting:
                    break;
                case GameState.Restart:
                    break;
                case GameState.RunningGameFinish:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, $"No implementation of {state} is found");
            }

            GameStateChanged?.Invoke(state);
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