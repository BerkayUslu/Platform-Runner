using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class Coin : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int _coinValue;

        private Transform _transform;
        private void Start()
        {
            _transform = transform;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.CompareTag(Tags.Player)) return;
            
            CoinAnimationsManager.Instance.PlayCoinAnimation();
            PlayerStatsManager.Instance.AddCoinWithDelay(1.5f);
            gameObject.SetActive(false);
        }
    }
}