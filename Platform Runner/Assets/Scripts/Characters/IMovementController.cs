using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public interface IMovementController
    {
        public event Action Moved;
        public event Action Stopped;

        public void DisableMovement();
        public void EnableMovement();
    }
}