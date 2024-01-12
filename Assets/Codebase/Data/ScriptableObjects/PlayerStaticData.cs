using UnityEngine;

namespace Codebase.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "StaticData/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        public float MaxHealth;
        public float MaxHealthUpgradeValue;
        public int MaxHealthPriceUpgrade;
        public float RegenValue;
        public float RegenUpgradeValue;
        public int RegenPriceUpgrade;
        public float DefenseValue;
        public float DefenseUpgradeValue;
        public int DefensePriceUpgrade;
        public float ShootSpeedValue;
        public float ShootSpeedUpgradeValue;
        public int ShootSpeedPriceUpgrade;
        public float DamageValue;
        public float DamageUpgradeValue;
        public int DamagePriceUpgrade;
        public float AttackRangeValue;
        public float AttackRangeUpgradeValue;
        public int AttackRangePriceUpgrade;
    }
}