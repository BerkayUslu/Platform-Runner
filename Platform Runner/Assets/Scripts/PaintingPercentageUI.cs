using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlatformRunner
{
    public class PaintingPercentageUI : MonoBehaviour
    {

        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            PaintingProgressChanged(0);
            PaintingManager.Instance.OnPaintingProgressChanged += PaintingProgressChanged;
        }

        private void OnDestroy()
        {
            PaintingManager.Instance.OnPaintingProgressChanged -= PaintingProgressChanged;
        }

        private void PaintingProgressChanged(float percentage)
        {
            _text.text = $"Painting Percentage: {(int)percentage}";
        }
    }
}