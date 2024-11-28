using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace PlatformRunner
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance;
        [SerializeField] private GameObject _startMenu;
        [SerializeField] private GameObject _inGameUI;
        [SerializeField] private GameObject _joystickUI;
        [SerializeField] private GameObject _paintingUI;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            _startMenu.SetActive(state == GameState.StartMenu);
            _inGameUI.SetActive(state == GameState.RunningGame);
            _joystickUI.SetActive(state == GameState.RunningGame);
            _paintingUI.SetActive(state == GameState.WallPainting);
        }
    }
}