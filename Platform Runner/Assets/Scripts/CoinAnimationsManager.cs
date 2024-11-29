using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    [RequireComponent(typeof(CoinAnimationPool))]
    public class CoinAnimationsManager : SingletonMonobehaviour<CoinAnimationsManager>
    {
        private CoinAnimationPool _coinAnimationPool;

        private void Start()
        {
            _coinAnimationPool = GetComponent<CoinAnimationPool>();
        }

        public void PlayCoinAnimation()
        {
            _coinAnimationPool.GetFromPool().PlayCoinAnimation();
        }
    }
}