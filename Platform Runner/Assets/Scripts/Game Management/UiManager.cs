using UnityEngine;
using System;
using Unity.VisualScripting;

namespace PlatformRunner.UI
{
    public class UiManager : SingletonMonobehaviour<UiManager>
    {
        public event Action OnHideAll;
        public event Action OnShowMenu;
        public event Action OnShowInGame;
        public event Action OnShowPainting;
        public event Action OnShowJoystick;
        public event Action OnShowAnimation;

        protected override void Awake()
        {
            base.Awake();
        }

        public void HideAll()
        {
            OnHideAll?.Invoke();
        }

        public void ShowMenu()
        {
            OnShowMenu?.Invoke();
        }

        public void ShowInGame()
        {
            OnShowInGame?.Invoke();
            OnShowJoystick?.Invoke();
        }

        public void ShowPainting()
        {
            OnShowPainting?.Invoke();
        }

        public void ShowAnimation()
        {
            OnShowAnimation?.Invoke();
        }
    }
}