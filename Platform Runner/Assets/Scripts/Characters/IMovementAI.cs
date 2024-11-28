using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public interface IMovementAI
    {
        public void MoveToPosition(Vector3 position);
    }
}