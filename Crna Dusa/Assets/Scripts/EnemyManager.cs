using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace crna
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimationManager;
        EnemyStats enemyStats;

        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;

        public State currentState;
        public CharacterStats currentTarget;

        public bool isPerformingAction;
        public bool isInteracting;

        public float rotationSpeed = 15;
        public float maximumAttackRange=1.5f;

        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            navMeshAgent.enabled = false;
            enemyRigidBody = GetComponent<Rigidbody>();            
        }

        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }
        private void Update()
        {
            HandleRecoveryTimer();

            isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
        }

        private void FixedUpdate()
        {
            HandleStateMachine();
        }

        private void HandleStateMachine()
        {
            if (currentState!=null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }


        private void SwitchToNextState(State state)
        {
            currentState = state;
        }
        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime>0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        
    }
}