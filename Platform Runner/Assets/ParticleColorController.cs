using UnityEngine;
using System.Collections.Generic;

namespace PlatformRunner
{
    public class ParticleColorController : MonoBehaviour
    {
        [Header("Color Settings")]
        [SerializeField]
        private List<Color> _possibleColors = new List<Color>
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            Color.magenta
        };

        [Header("References")]
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            if (_particleSystem == null)
            {
                _particleSystem = GetComponentInChildren<ParticleSystem>();
                if (_particleSystem == null)
                {
                    Debug.LogError("ParticleColorController: No ParticleSystem found in children");
                }
            }
        }

        public void ChangeToRandomColor()
        {
            if (_particleSystem == null || _possibleColors.Count == 0) return;

            var mainModule = _particleSystem.main;
            mainModule.startColor = _possibleColors[Random.Range(0, _possibleColors.Count)];
        }
    }
}