using Codebase.Logic;
using Codebase.Logic.Damage;
using Codebase.Player;
using UnityEngine;

namespace Codebase.Enemy
{
    public class EnemyDamage : MonoBehaviour, IDamageInfo
    {
        private float _damage;

        [SerializeField] private DestroyableEntity _destroyable;
        
        public float Damage
        {
            get => _damage;
            private set => _damage = value;
        }
        
        public void Construct(float damage)
        {
            Damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Tower>(out Tower tower))
            {
                _destroyable.Destroy();
            }
        }
    }
}