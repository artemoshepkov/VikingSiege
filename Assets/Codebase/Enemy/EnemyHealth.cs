using System;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public float CurrentHp { get; set; }
        public float MaxHp { get; set; }
        
        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentException();

            if (CurrentHp <= 0)
                return;
            
            CurrentHp -= damage;
            HealthChanged?.Invoke();
        }
    }
}