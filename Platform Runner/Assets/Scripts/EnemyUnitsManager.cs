using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class EnemyUnitsManager : MonoBehaviour
    {
        public EnemyController[] _enemyControllers;

        private void Start()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.RunningGame)
            {
                InitiateTheEnemeyUnits();
            }

        }

        private void InitiateTheEnemeyUnits()
        {
            Vector3 targetPosition = FindObjectOfType<DestinationObject>().GetPosition();

            foreach (EnemyController enemy in _enemyControllers)
            {
                enemy.InitializeRunningTowardsTarget(targetPosition);
            }
        }

        private void OnValidate()
        {
            _enemyControllers = FindObjectsOfType<EnemyController>();
        }
    }
}