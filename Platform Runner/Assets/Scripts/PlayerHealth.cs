using UnityEngine;
using System;
using PlatformRunner.Core;
using System.Collections;

namespace PlatformRunner.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public event Action Died;
        public bool IsDead { get; private set; }

        [SerializeField] private float _respawnDelay = 1.5f;

        private void OnEnable()
        {
            IsDead = false;
        }

        public void KillCharacter()
        {
            if (IsDead) return;

            IsDead = true;
            Died?.Invoke();
            PlayerStatsManager.Instance.IncreaseFail();
            StartCoroutine(nameof(RespawnAfterDelay));
        }

        private IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(_respawnDelay);
            GameManager.Instance.RestartCurrentScene();
        }
    }
}