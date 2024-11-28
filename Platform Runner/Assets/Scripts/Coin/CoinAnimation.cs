using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class CoinAnimation : MonoBehaviour
    {
        [SerializeField] private CoinImage[] _coinImages;
        [SerializeField] private float _coinAmountTargetTime;
        [SerializeField] private RectTransform[] _firstTargetPositions;
        [SerializeField] private float _firstTargetTime;
        private RectTransform _coinAmountTargetPosition;
        private int _completedCoinImageAnimations = 0;

        private CoinAnimationPool _coinAnimationPool;

        private void Awake()
        {
            if (_firstTargetPositions.Length < _coinImages.Length)
                Debug.LogWarning("Not enough first target for current coin image amount");
        }

        public void Init(RectTransform coinAmountTargetPosition, CoinAnimationPool coinAnimationPool)
        {
            _coinAmountTargetPosition = coinAmountTargetPosition;
            _coinAnimationPool = coinAnimationPool;
        }

        public void PlayCoinAnimation()
        {
            for (int index = 0; index < _coinImages.Length; index++)
            {
                _coinImages[index].Reset();
                _coinImages[index].Animate(index,
                 _firstTargetPositions[index].position, _firstTargetTime,
                 _coinAmountTargetPosition.position, _coinAmountTargetTime).OnComplete(() => CoinImageAnimationCompleted());
            }
        }

        private void CoinImageAnimationCompleted()
        {
            _completedCoinImageAnimations++;
            if (_completedCoinImageAnimations == _coinImages.Length)
            {
                _completedCoinImageAnimations = 0;
                _coinAnimationPool.AddBackToPool(this);
            }
        }
    }
}