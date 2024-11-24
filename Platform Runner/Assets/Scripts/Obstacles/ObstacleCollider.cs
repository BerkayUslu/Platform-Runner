using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    public class ObstacleCollider : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _trigerred;
        [SerializeField] private UnityEvent<Collision> _collided;


        private void OnTriggerEnter(Collider collider)
        {
            _trigerred?.Invoke(collider);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _collided?.Invoke(collision);
        }

    }
}