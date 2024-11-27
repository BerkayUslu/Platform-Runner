using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class CoinAnimationPool : MonoBehaviour
    {
        [SerializeField] private GameObject _coinAnimationPrefab;
        [SerializeField] private RectTransform _coinAnimationTargetPosition;
        [SerializeField] private RectTransform _animationParent;

        private Queue<CoinAnimation> _coinAnimations;

        private void Awake()
        {
            _coinAnimations = new Queue<CoinAnimation>();
        }
        
        public CoinAnimation GetFromPool()
        {
            if (_coinAnimations.Count > 0)
            {
                return _coinAnimations.Dequeue();
            }

            return CreateCoinAnimation();
        }

        public void AddBackToPool(CoinAnimation coinAnimation)
        {
            _coinAnimations.Enqueue(coinAnimation);
        }

        private CoinAnimation CreateCoinAnimation()
        {
            var temp = Instantiate(_coinAnimationPrefab, _animationParent).GetComponent<CoinAnimation>();
            temp.Init(_coinAnimationTargetPosition, this);
            return temp;
        }

    }
}