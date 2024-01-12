using System;
using Codebase.Logic.Stats;

namespace Codebase.Player.Stats
{
    public class ShootSpeed : IUpgradableStat
    {
        private float _minCooldown = 0.5f;
        
        private float _upgradeableValue;

        public ShootSpeed(float coolDown, int price, float upgradeableValue)
        {
            _upgradeableValue = upgradeableValue;
            CoolDown = coolDown;
            Price = price;
        }

        public float CoolDown
        {
            get;
            private set;
        }

        public int Price { get; private set; }

        public object Value => Math.Round(CoolDown, 2);

        public void Upgrade()
        {
            if (CoolDown < _minCooldown)
                return;

            CoolDown -= _upgradeableValue;
        }
    }
}