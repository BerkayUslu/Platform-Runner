using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    public class RotatingStick : MonoBehaviour
    {
        [SerializeField] private Transform _pointOnStick;
        [SerializeField] private UnityEvent<Collider, Transform> _triggered;

        private void OnCollisionEnter(Collision collision)
        {
            _triggered?.Invoke(collision.collider, _pointOnStick);
        }
    }
}