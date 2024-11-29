using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlatformRunner
{
    public class PositionText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            RunningRaceManager.Instance.PlayerPositionChanged += SetPositionText;
        }

        private void OnDestroy()
        {
            RunningRaceManager.Instance.PlayerPositionChanged -= SetPositionText;
        }

        public void SetPositionText(int pos)
        {
            _text.text = $"POS {pos} / 11";
        }
    }
}