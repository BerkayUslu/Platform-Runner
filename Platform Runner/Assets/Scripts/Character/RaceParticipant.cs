using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

namespace PlatformRunner
{
    public class RaceParticipant : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
        public float GetPositionZ() => _transform.position.z;
    }
}