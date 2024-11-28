using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

namespace PlatformRunner
{
    public class PlayerStatsManager : MonoBehaviour
    {
        public static PlayerStatsManager Instance;
        public static event Action<int> CoinAmountChanged;
        public static event Action<int> FailAmountChanged;

        private readonly string FAIL_AMOUNT = "FailAmount";
        private readonly string COIN_AMOUNT = "CoinAmount";

        private int _failAmount = 0;
        private int _coinAmount = 0;

        public int FailAmount { get => _failAmount; }
        public int CoinAmount { get => _coinAmount; }

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
            LoadAmounts();
        }

        private void OnDestroy()
        {
            SaveAmounts();
        }

        public void AddCoin()
        {
            _coinAmount++;
            CoinAmountChanged?.Invoke(CoinAmount);
        }

        public void AddCoinWithDelay(float time)
        {
            Invoke("AddCoin", time);
        }


        public void IncreaseFail()
        {
            _failAmount++;
            FailAmountChanged?.Invoke(_failAmount);
        }

        private void LoadAmounts()
        {
            _coinAmount = PlayerPrefs.GetInt(COIN_AMOUNT, 0);
            _failAmount = PlayerPrefs.GetInt(FAIL_AMOUNT, 0);
        }

        private void SaveAmounts()
        {
            PlayerPrefs.SetInt(COIN_AMOUNT, _coinAmount);
            PlayerPrefs.SetInt(FAIL_AMOUNT, _failAmount);
            PlayerPrefs.Save();
        }
    }
}