using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public interface IHealth
    {
        public bool Dead { get;}
        public event Action Died;
    }
}