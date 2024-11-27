using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformRunner
{
    public class CoinImage : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Vector3 _initialPosition;
        private float _delayConstant = 0.05f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.position;
        }

        public Sequence Animate(int index, Vector2 firstTargetPosition, float firstTargetTime, Vector2 _coinAmountTargetPosition, float _coinAmountTargetTime)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Join(_rectTransform.DOMove(firstTargetPosition, firstTargetTime)
                .SetDelay(index * _delayConstant));

            sequence.Join(_rectTransform.DOScale(1.5f, firstTargetTime)
                .SetDelay(index * _delayConstant)
                .SetEase(Ease.OutElastic));

            sequence.Append(_rectTransform.DOJump(_coinAmountTargetPosition, 200, 1, _coinAmountTargetTime));

            sequence.Join(_rectTransform.DOScale(1f, _coinAmountTargetTime));

            return sequence.Play();
        }

        public void Reset()
        {
            _rectTransform.localScale = Vector3.zero;
            _rectTransform.position = _initialPosition;
        }


    }
}