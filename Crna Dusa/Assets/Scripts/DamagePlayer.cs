using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace crna
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        private void OnTriggerEnter(Collider other)
        {
            EnemyStats playerStats = other.GetComponent<EnemyStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                Debug.Log("damage");
            }
        }

    }
}
