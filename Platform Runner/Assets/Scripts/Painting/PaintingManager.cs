using UnityEngine;
using System;
using Unity.VisualScripting;
using PlatformRunner.Core;
using PlatformRunner.Core.StateMachine;

namespace PlatformRunner
{
    public class PaintingManager : SingletonMonobehaviour<PaintingManager>
    {
        public event Action<float> OnPaintingProgressChanged;
        public event Action OnPaintingEnabled;

        public void PaintingProgressChanged(float percentage)
        {
            if (percentage == 100)
            {
                GameManager.Instance.ChangeState<CelebrateState>();
            }

            OnPaintingProgressChanged?.Invoke(percentage);
        }

        public void EnablePainting()
        {
            OnPaintingEnabled?.Invoke();
        }
    }
}
