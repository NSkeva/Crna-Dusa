using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using crna;
public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {            
            if (currentHealth > -100)
            {
                animator.Play("Death_01");
            }
            currentHealth = -100;
        }
        if (currentHealth > -100)
        {
            animator.Play("Damage_01");
        }
    }
}
