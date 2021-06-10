using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crna;

namespace crna
{
    public class PlayerStats : CharacterStats
    {
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
        }
        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            maxStamina = SetMaxStaminaFromStaminaLevel();
            currnetStamina = maxStamina;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Damage_01", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death_01", true);
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currnetStamina = currnetStamina - damage;
            //staminaBar.SetCurrentStamina(currnetStamina);

        }
    }
}