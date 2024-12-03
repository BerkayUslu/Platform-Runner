using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public interface IObstacle
    {
        public void TryKillCollidedObject(Collider collider);
    }
}
