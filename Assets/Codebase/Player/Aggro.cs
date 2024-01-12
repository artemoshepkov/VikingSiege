using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codebase.Player
{
    public class Aggro : MonoBehaviour
    {
        private List<Enemy.Enemy> _enemies =
            new List<Enemy.Enemy>();

        [SerializeField] private TriggerEnemyObserver _observer;
        [SerializeField] private TowerShoot _towerShoot;

        private void Start()
        {
            _observer.TriggerEntered += AddEnemy;
            _observer.TriggerExited += RemoveEnemy;
        }

        private void Update()
        {
            if (_enemies.Count <= 0)
                return;

            SetEnemyForShooting();
        }

        private void OnDestroy()
        {
            _observer.TriggerEntered -= AddEnemy;
            _observer.TriggerExited -= RemoveEnemy;
        }

        private void AddEnemy(Enemy.Enemy enemy)
        {
            if (!_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
            }
        }

        private void RemoveEnemy(Enemy.Enemy enemy)
        {
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }

        private void SetEnemyForShooting()
        {
            Enemy.Enemy selectedEnemy = SelectEnemy();
            _towerShoot.Target = selectedEnemy == null ? null : selectedEnemy.transform;
        }

        private Enemy.Enemy SelectEnemy()
        {
            Enemy.Enemy currentEnemy = _enemies.First();

            Vector3 towerPosition = this.transform.position;

            foreach (Enemy.Enemy enemy in _enemies)
            {
                Vector3 enemyPosition = enemy.gameObject.transform.position;

                if (Vector3.Distance(enemyPosition, towerPosition)
                    < Vector3.Distance(currentEnemy.transform.position, towerPosition))
                {
                    currentEnemy = enemy;
                }
            }

            return currentEnemy;
        }
    }
}