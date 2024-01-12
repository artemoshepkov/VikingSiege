using Codebase.Logic.Damage;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Player.Stats
{
    public class TowerDamage : MonoBehaviour, IDamageInfo, IUpgradableStat
    {
        private float _damage;
        private float _upgradeValue;

        public float Damage => _damage;
        public int Price { get; private set; }
        public object Value => _damage;
        
        public void Upgrade()
        {
            _damage += _upgradeValue;
        }

        public void Construct(float damageValue, int price, float upgradeValue)
        {
            _damage = damageValue;
            Price = price;
            _upgradeValue = upgradeValue;
        }
    }
}