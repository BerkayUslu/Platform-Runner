using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    public class ObstacleCollider : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _collided;

        void OnTriggerEnter(Collider collider)
        {
            _collided?.Invoke(collider);
        }
    }
}