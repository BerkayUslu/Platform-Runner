using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner.UI
{
    public class JoystickCanvas : MonoBehaviour
    {
        private void Start()
        {
            UiManager.Instance.OnHideAll += () => gameObject.SetActive(false);
            UiManager.Instance.OnShowJoystick += () => gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            UiManager.Instance.OnHideAll -= () => gameObject.SetActive(false);
            UiManager.Instance.OnShowJoystick -= () => gameObject.SetActive(true);
        }
    }

}
