using System;
using Codebase.Logic.Damage;
using Codebase.Logic.Stats;

namespace Codebase.Player.Stats
{
    public class Defense : DamageableDecorator, IUpgradableStat
    {
        private float _defenseValue;
        private float _upgradeValue;

        public int Price { get; private set; }
        public object Value => Math.Round(_defenseValue, 2);

        public Defense(IDamageable damageable, float defenseValue, float upgradeValue, int price) : base(damageable)
        {
            _defenseValue = defenseValue;
            _upgradeValue = upgradeValue;
            Price = price;
        }

        public override void TakeDamage(float damage)
        {
            float lessDamage = damage * (1f - _defenseValue);
            _damageable.TakeDamage(lessDamage);    
        }

        public void Upgrade()
        {
            if (_defenseValue > 1)
            {
                return;
            }
            
            _defenseValue += _upgradeValue;
        }
    }
}