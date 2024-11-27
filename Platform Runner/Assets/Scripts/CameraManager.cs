using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using PlatformRunner.Core;

namespace PlatformRunner
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _gameCamera;
        [SerializeField] private CinemachineVirtualCamera _paintingCamera;

        public void DeactivateGameCamera()
        {
            _gameCamera.gameObject.SetActive(false);
        }
    }
}