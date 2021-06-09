using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace crna
{
    public class EnemyManager : CharacterStats
    {
        EnemyLocomotionManager enemyLocomotionManager;
        bool isPerformingAction;

        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        }
        private void Update()
        {
            HandleCurrentAction();
        }

        private void HandleCurrentAction()
        {
            if (enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandleDetection();
            }
        }
    }
}