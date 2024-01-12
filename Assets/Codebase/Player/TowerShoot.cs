using Codebase.Logic;
using Codebase.Logic.ObjectsPool;
using Codebase.Player.Stats;
using UnityEngine;

namespace Codebase.Player
{
    public class TowerShoot : MonoBehaviour
    {
        private float _attackingCooldown;

        private ShootSpeed _shootSpeed;
        private PoolBase<GameObject> _missilesPool;

        public Transform Target { get; set; }

        public void Construct(ShootSpeed shootSpeed, PoolBase<GameObject> missilesPool)
        {
            _shootSpeed = shootSpeed;
            _missilesPool = missilesPool;
        }

        private void Update()
        {
            UpdateCooldown();

            if (Target != null && Target.gameObject.activeSelf && CooldownIsUp())
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Missile missile = _missilesPool.Get().GetComponent<Missile>();

            missile.GetComponent<DestroyableEntity>().Construct(() => _missilesPool.Return(missile.gameObject));
            
            missile.Launch(transform.position, Target.position);

            _attackingCooldown = _shootSpeed.CoolDown;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackingCooldown -= Time.deltaTime;
        }

        private bool CooldownIsUp() => _attackingCooldown <= 0;
    }
}