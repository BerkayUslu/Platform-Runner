using System.Collections;
using PlatformRunner.Core;
using UnityEngine;

namespace PlatformRunner
{
    public class EnemyUnitsManager : MonoBehaviour
    {
        public EnemyController[] _enemyControllers;

        public void InitiateTheEnemeyUnits()
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