using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformRunner
{
    [RequireComponent(typeof(Slider))]
    public class BrushSizeSlider : MonoBehaviour
    {
        [SerializeField] private Transform _knob;
        [SerializeField] private float _minKnobScaleValue;
        [SerializeField] private float _maxKnobScaleValue;
        private Slider _slider;

        private Vector3 _minKnobScale;
        private Vector3 _maxKnobScale;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(ChangeKnobSize);
            _minKnobScale = Vector3.one * _minKnobScaleValue;
            _maxKnobScale = Vector3.one * _maxKnobScaleValue;
        }

        private void ChangeKnobSize(float value)
        {
            _knob.localScale = Vector3.Lerp(_minKnobScale, _maxKnobScale, value);
        }

    }
}