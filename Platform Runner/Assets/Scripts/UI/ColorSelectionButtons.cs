using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class ColorSelectionButtons : MonoBehaviour
    {
        [SerializeField] private BrushColorButton[] _brushColorButtons;
        [SerializeField] private Brush _brush;

        private BrushColorButton __selectedColorButton;

        private void Start()
        {
            if (_brush == null)
            {
                _brush = FindObjectOfType<Brush>();
            }

            foreach (BrushColorButton colorButton in _brushColorButtons)
            {
                colorButton.Init(this);
            }

            __selectedColorButton = _brushColorButtons[0];
            __selectedColorButton.EnableIndicator();
        }

        public void ChangeColor(Color color, BrushColorButton button)
        {
            if (__selectedColorButton != null)
            {
                __selectedColorButton.DisableIndicator();
            }

            __selectedColorButton = button;
            __selectedColorButton.EnableIndicator();
            
            _brush.SetBrushColor(color);
        }
    }
}