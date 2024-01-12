using System;
using System.Collections;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Player.Stats
{
    public class RegenerationHealth : MonoBehaviour, IUpgradableStat
    {
        private Coroutine _coroutine;
        private float _timeBetweenRegen = 0.2f;
        private float _regenValue;
        private float _upgradeValue;

        [SerializeField] private TowerHealth health;

        private float RegenValue => _regenValue / 5f;

        public int Price { get; private set; }
        public object Value => Math.Round(_regenValue, 2);
        
        private void Start() => _coroutine = StartCoroutine(Regen());

        private void OnDestroy() => StopCoroutine(_coroutine);

        private IEnumerator Regen()
        {
            while (true)
            {
                if (health.CurrentHp >= health.MaxHp)
                {
                    yield return new WaitForSeconds(_timeBetweenRegen);
                    continue;
                }

                if (health.CurrentHp >= health.MaxHp - RegenValue)
                {
                    health.CurrentHp = health.MaxHp;
                    yield return new WaitForSeconds(_timeBetweenRegen);
                    continue;
                }

                health.CurrentHp += RegenValue;
                yield return new WaitForSeconds(_timeBetweenRegen);
            }
        }

        public void Upgrade()
        {
            _regenValue += _upgradeValue;
        }

        public void Construct(float regenValue, int price, float regenUpgradeValue)
        {
            _regenValue = regenValue;
            Price = price;
            _upgradeValue = regenUpgradeValue;
        }
    }
}