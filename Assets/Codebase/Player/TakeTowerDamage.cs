using Codebase.Logic.Damage;
using UnityEngine;

namespace Codebase.Player
{
    public class TakeTowerDamage : MonoBehaviour
    {
        private IDamageable _damageable;

        public void Construct(IDamageable damageable)
        {
            _damageable = damageable;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<IDamageInfo>(out IDamageInfo damageable))
            {
                _damageable.TakeDamage(damageable.Damage);
            }
        }
    }
}