using System;
using UnityEngine;

namespace Codebase.Player
{
    public class TriggerEnemyObserver : MonoBehaviour
    {
        public event Action<Enemy.Enemy> TriggerEntered;
        public event Action<Enemy.Enemy> TriggerExited;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy))
            {
                TriggerEntered?.Invoke(enemy);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy))
            {
                TriggerExited?.Invoke(enemy);
            }
        }
    }
}