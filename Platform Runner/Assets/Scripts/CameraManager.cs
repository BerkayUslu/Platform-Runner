using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace PlatformRunner
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _gameCamera;
        [SerializeField] private CinemachineVirtualCamera _paintingCamera;

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
            if (state == GameState.WallPainting)
                _gameCamera.gameObject.SetActive(false);
        }
    }
}