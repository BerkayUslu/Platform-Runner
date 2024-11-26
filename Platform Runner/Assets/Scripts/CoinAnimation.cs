using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlatformRunner
{
    public class CoinAnimation : MonoBehaviour
    {
        [SerializeField] private CoinImage[] _coinImages;
        [SerializeField] private RectTransform _coinAmountTargetPosition;
        [SerializeField] private float _coinAmountTargetTime;
        [SerializeField] private RectTransform[] _firstTargetPositions;
        [SerializeField] private float _firstTargetTime;

        private void Awake()
        {
            if (_firstTargetPositions.Length < _coinImages.Length)
                Debug.LogWarning("Not enough first target for current coin image amount");
        }

        public void PlayCoinAnimation()
        {
            for (int index = 0; index < _coinImages.Length; index++)
            {
                _coinImages[index].Reset();
                _coinImages[index].Animate(index,
                 _firstTargetPositions[index].position, _firstTargetTime,
                 _coinAmountTargetPosition.position, _coinAmountTargetTime);
            }
        }
    }
}