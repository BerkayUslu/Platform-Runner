using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class ShiningObstacle : ObstacleBase
    {
        private Transform _transform;
        private Sequence _leftRightMovementSequence;
        private ParticleColorController _particleColorController;

        private void Awake()
        {
            _transform = transform;
            _particleColorController = GetComponent<ParticleColorController>();

            if (_particleColorController == null)
            {
                Debug.LogWarning("ShiningObstacle: ParticleColorController component not found");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryKillCollidedObject(collision.collider);
        }
        
    }
}