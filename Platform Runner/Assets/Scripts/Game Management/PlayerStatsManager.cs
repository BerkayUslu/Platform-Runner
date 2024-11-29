using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

namespace PlatformRunner
{
    public class PlayerStatsManager : SingletonMonobehaviour<PlayerStatsManager>
    {
        public static event Action<int> CoinAmountChanged;
        public static event Action<int> FailAmountChanged;

        private readonly string FAIL_AMOUNT = "FailAmount";
        private readonly string COIN_AMOUNT = "CoinAmount";

        private int _failAmount = 0;
        private int _coinAmount = 0;

        public int FailAmount { get => _failAmount; }
        public int CoinAmount { get => _coinAmount; }

        private void Start()
        {
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

        public void ResetStats()
        {
            _coinAmount = 0;
            _failAmount = 0;
            
            SaveAmounts();
            
            CoinAmountChanged?.Invoke(_coinAmount);
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