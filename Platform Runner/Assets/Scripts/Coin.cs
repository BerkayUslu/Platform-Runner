using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class Coin : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _halfTurnTime;
        [SerializeField] private bool _spinReverse;
        [SerializeField] private int _coinValue;

        private float _halfTurn = 180;
        private Transform _transform;
        private void Start()
        {
            if (_spinReverse)
                _halfTurn = -_halfTurn;

            _transform = transform;
            RotateCoin();

        }

        private void RotateCoin()
        {
            _transform.DOLocalRotate(Vector3.up * _halfTurn, _halfTurnTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.Player))
            {
                CoinAnimationsManager.Instance.PlayCoinAnimation();
                PlayerStatsManager.Instance.AddCoinWithDelay(1.5f);
                gameObject.SetActive(false);
            }
        }
    }
}