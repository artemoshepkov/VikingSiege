using System;
using Codebase.Data;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth EnemyHealth;
        private EnemiesData _enemiesData;

        [SerializeField] private DestroyableEntity _destroyable;
        
        public event Action Died;
        
        public void Construct(EnemiesData enemiesData) => _enemiesData = enemiesData;

        private void Start() => EnemyHealth.HealthChanged += CheckDeath;

        private void OnDestroy() => EnemyHealth.HealthChanged -= CheckDeath;

        private void CheckDeath()
        {
            if (EnemyHealth.CurrentHp <= 0) 
                Die();
        }

        private void Die()
        {
            _destroyable.Destroy();
            _enemiesData.AmountKilled += 1;
            Died?.Invoke();
        }
    }
}