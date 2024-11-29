using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PlatformRunner
{
    public class StartCountdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string[] _countdownStrings;

        private RectTransform _textTransform;

        private void Start()
        {
            _textTransform = _text.GetComponent<RectTransform>();
            _textTransform.localScale = Vector3.zero;
        }

        public Sequence AnimateCountdown(float textGrowTime, float getSmallerDelay, float getSmallerTime)
        {
            _textTransform.localScale = Vector3.zero;
            Sequence countdownSequence = DOTween.Sequence();

            if (_countdownStrings.Length == 0)
            {
                Debug.LogError("No string found in countdownStrings");
                return null;
            }

            _text.text = _countdownStrings[0];
            for (int i = 0; i < _countdownStrings.Length; i++)
            {
                int currentIndex = i;
                countdownSequence.Append(_textTransform
                    .DOScale(Vector3.one, textGrowTime)
                    .SetEase(Ease.OutElastic));

                countdownSequence.Append(_textTransform
                    .DOScale(Vector3.zero, getSmallerTime)
                    .SetDelay(getSmallerDelay));

                countdownSequence.AppendCallback(() =>
                    {
                        Debug.Log(currentIndex);
                        if (currentIndex + 1 < _countdownStrings.Length)
                            _text.text = _countdownStrings[currentIndex + 1];
                    });

            }

            return countdownSequence.Play();
        }
    }
}