using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Core;
using UnityEngine;

namespace PlatformRunner.UI
{
    public class UiManager : MonoBehaviour
    {
        [Header("Canvas References")]
        [SerializeField] private GameObject _startMenu;
        [SerializeField] private GameObject _inGameUI;
        [SerializeField] private GameObject _joystickUI;
        [SerializeField] private GameObject _paintingUI;

        private GameObject[] _uiElements;

        private void Awake()
        {
            _uiElements = new[] { _startMenu, _inGameUI, _joystickUI, _paintingUI };
        }
        
        public void HideAll()
        {
            foreach (var element in _uiElements)
            {
                element.SetActive(false);
            }
        }

        public void ShowMenu()
        {
            HideAll();
            _startMenu.SetActive(true);
        }

        public void ShowInGame()
        {
            HideAll();
            _inGameUI.SetActive(true);
            _joystickUI.SetActive(true);
        }

        public void ShowPainting()
        {
            HideAll();
            _paintingUI.SetActive(true);
        }
    }
}