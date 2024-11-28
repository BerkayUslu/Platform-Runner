using System.Collections;
using System.Collections.Generic;
using PlatformRunner;
using PlatformRunner.Core;
using Unity.VisualScripting;
using UnityEngine;

public class LowerMapBound : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        PlayerStatsManager.Instance.IncreaseFail();
        GameManager.Instance.RestartCurrentScene();
    }
}
