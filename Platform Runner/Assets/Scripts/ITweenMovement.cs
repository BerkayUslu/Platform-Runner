using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public interface ITweenMovement
    {
        public Tween MoveToPosition(Vector3 position, float time);
    }
}