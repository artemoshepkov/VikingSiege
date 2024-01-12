using System;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Player.Stats
{
    public class AttackRange : MonoBehaviour, IUpgradableStat 
    {
        private float _upgradeValue;
        
        [SerializeField] private CircleCollider2D _rangeCollider;

        public float RangeRadius
        {
            get => _rangeCollider.radius;
            private set => _rangeCollider.radius = value;
        }
        public int Price { get; private set; }
        public object Value => Math.Round(RangeRadius, 2);

        public void Construct(float attackRangeValue, int price, float upgradeValue)
        {
            RangeRadius = attackRangeValue;
            Price = price;
            _upgradeValue = upgradeValue;
        }

        public void Upgrade()
        {
            RangeRadius += _upgradeValue;
        }
    }
}