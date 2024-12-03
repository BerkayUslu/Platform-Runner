using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class StaticObstacle : ObstacleBase
    {
        private void OnCollisionEnter(Collision collision)
        {
           TryKillCollidedObject(collision.collider);
        }
    }
}