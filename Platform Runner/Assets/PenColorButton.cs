using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlatformRunner
{
    public class PenColorButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Color _color;
        [SerializeField] private Pen _pen;

        private void Start()
        {
            if (_pen == null)
            {
                Debug.LogWarning("Pen reference is not set");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pen.SetPenColor(_color);
        }
    }
}