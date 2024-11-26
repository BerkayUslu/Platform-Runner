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
        private CoinAnimation _coinAnimation;

        private void Start()
        {
            if (_spinReverse)
                _halfTurn = -_halfTurn;

            _transform = transform;
            RotateCoin();

            _coinAnimation = FindObjectOfType<CoinAnimation>();
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
                if (_coinAnimation == null)
                {
                    Debug.LogError("Coin animation not found");
                    return;
                }
                _coinAnimation.PlayCoinAnimation();
                gameObject.SetActive(false);
            }
        }

    }
}