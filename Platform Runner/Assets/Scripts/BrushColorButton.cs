using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlatformRunner
{
    public class BrushColorButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Color _color;
        [SerializeField] private Brush _brush;

        private void Start()
        {
            if (_brush == null)
            {
                Debug.LogWarning("Pen reference is not set");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _brush.SetPenColor(_color);
        }
    }
}