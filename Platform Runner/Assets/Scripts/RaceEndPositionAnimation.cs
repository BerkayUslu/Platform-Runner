using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PlatformRunner
{
    public class RaceEndPositionAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _finalPositionTextRect;
        [SerializeField] private TMP_Text _rankText;

        private RectTransform _rankTextRect;


        private void Start()
        {
            _rankTextRect = _rankText.GetComponent<RectTransform>();
            ScaleElementsToZero();
        }

        private void ScaleElementsToZero()
        {
            _finalPositionTextRect.localScale = Vector3.zero;
            _rankTextRect.localScale = Vector3.zero;
        }

        public void AnimateFinalPosition(int finalPosition, float rankScaleUpTime, float rankTextDelay, float finalPosScaleUpTime)
        {
            _rankText.text = finalPosition.ToString();

            _rankTextRect.DOScale(Vector3.one, rankScaleUpTime).SetEase(Ease.OutElastic).SetDelay(rankTextDelay);
            _finalPositionTextRect.DOScale(Vector3.one, finalPosScaleUpTime).SetEase(Ease.OutElastic);
        }
    }
}