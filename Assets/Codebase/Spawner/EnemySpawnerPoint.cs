using Codebase.Logic;
using Codebase.Logic.ObjectsPool;
using UnityEngine;

namespace Codebase.Spawner
{
    public class EnemySpawnerPoint : MonoBehaviour
    {
        private PoolBase<GameObject> _enemiesPool;

        public void Construct(PoolBase<GameObject> enemiesPool)
        {
            _enemiesPool = enemiesPool;
        }

        public void Spawn()
        {
            GameObject enemy = _enemiesPool.Get();
            enemy.transform.position = transform.position;
            enemy.GetComponent<DestroyableEntity>()
                .Construct(() => GameObjectsPool.ReturnAction(enemy));
        }
    }
}