using System;
using System.Collections;
using Codebase.Enemy;
using Codebase.Logic;
using Codebase.Logic.Damage;
using UnityEngine;

namespace Codebase.Player
{
    public class Missile : MonoBehaviour
    {
        private Coroutine _coroutine;
        
        private IDamageInfo _damageInfo;
        private float _speed = 0.1f;
        private Vector3 _direction;

        [SerializeField] private DestroyableEntity _destroyable;
        
        public float Damage => _damageInfo.Damage;
        
        public void Construct(IDamageInfo damageInfo) => _damageInfo = damageInfo;

        public void Launch(Vector3 spawnPosition, Vector3 targetPosition)
        {
            transform.position = spawnPosition;
            SetTarget(targetPosition);
        }
        
        private void FixedUpdate() => Move();

        private void OnEnable() => _coroutine = StartCoroutine(DestroyAfterTime());
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(Damage);
                StopCoroutine(_coroutine);
                _destroyable.Destroy();
            }
        }

        private void SetTarget(Vector3 targetPosition) => _direction = targetPosition - transform.position;

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(5f);
            _destroyable.Destroy();
        }

        private void Move() => gameObject.transform.Translate(_direction.normalized * _speed, Space.World);
    }
}