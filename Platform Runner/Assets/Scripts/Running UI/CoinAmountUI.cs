using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlatformRunner
{
    public class CoinAmountUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            UpdateCoinAmountText(PlayerStatsManager.Instance.CoinAmount);
            PlayerStatsManager.CoinAmountChanged += UpdateCoinAmountText;
        }

        private void OnDestroy()
        {
            PlayerStatsManager.CoinAmountChanged -= UpdateCoinAmountText;
        }

        public void UpdateCoinAmountText(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}