using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace PlatformRunner
{
    public class FailAmountUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            UpdateFailAmountText(PlayerStatsManager.Instance.FailAmount);
            PlayerStatsManager.FailAmountChanged += UpdateFailAmountText;
        }

        private void OnDestroy()
        {
            PlayerStatsManager.FailAmountChanged -= UpdateFailAmountText;
        }

        public void UpdateFailAmountText(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}