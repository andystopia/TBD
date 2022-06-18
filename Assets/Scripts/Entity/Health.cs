using System;
using Attack;
using UnityEngine;

namespace Entity
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth;

        public int MaxHealth => maxHealth;

        public bool IsDead => currentHealth <= 0;

        /// <summary>
        /// Changes the amount of health this component has by the given amount.
        ///
        /// Pass a negative value to deal damage.
        /// </summary>
        /// <param name="amount"> the amount to change the health by</param>
        public void ChangeHealth(int amount)
        {
            currentHealth += amount;
            currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        }

        public bool IsFullHealth => currentHealth == MaxHealth;
    }
}