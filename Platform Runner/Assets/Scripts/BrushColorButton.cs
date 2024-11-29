using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlatformRunner
{
    public class BrushColorButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Color _color;
        [SerializeField] private Image _selectedIndicator;
        private ColorSelectionButtons _manager;

        public void Init(ColorSelectionButtons manager)
        {
            _manager = manager;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _manager.ChangeColor(_color, this);
        }

        public void DisableIndicator()
        {
            _selectedIndicator.enabled = false;
        }

        public void EnableIndicator()
        {
            _selectedIndicator.enabled = true;
        }
    }
}