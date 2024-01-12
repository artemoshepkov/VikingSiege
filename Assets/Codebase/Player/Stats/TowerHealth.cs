using System;
using Codebase.Logic;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Player.Stats
{
    public class TowerHealth : MonoBehaviour, IHealth, IUpgradableStat
    {
        private float _currentHp;
        private float _maxHp;
        private float _upgradeValue;

        public float CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = value;
                _currentHp = (float) Math.Round(_currentHp, 2);
                HealthChanged?.Invoke();
            }
        }

        public float MaxHp
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        public int Price { get; private set; }
        public object Value => Math.Round(MaxHp, 2);

        public event Action HealthChanged;

        public void Construct(float maxHealth, int priceUpgrade,
            float upgradeValue)
        {
            MaxHp = maxHealth;
            CurrentHp = MaxHp;
            _upgradeValue = upgradeValue;
            Price = priceUpgrade;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHp <= 0)
                return;

            CurrentHp -= damage;
        }

        public void Upgrade()
        {
            MaxHp += _upgradeValue;
            CurrentHp += _upgradeValue;
            // Price *= GetPriceCoef(Price);
        }

        private float GetPriceCoef(float price) => 1.5f * (float) Math.Pow(price, 2);
    }
}