using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public interface IRotationDirection
    {
        public Vector3 GetDirectionAxisY();
        public Vector3 GetDirectionAxisZ();
    }
}