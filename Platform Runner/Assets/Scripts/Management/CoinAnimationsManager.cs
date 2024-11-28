using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    [RequireComponent(typeof(CoinAnimationPool))]
    public class CoinAnimationsManager : MonoBehaviour
    {
        public static CoinAnimationsManager Instance;
        private CoinAnimationPool _coinAnimationPool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

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