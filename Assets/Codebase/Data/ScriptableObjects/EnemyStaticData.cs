using Codebase.Enemy;
using Codebase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Codebase.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
        public GameObject Prefab;
        public float Damage;
        public float HP;
        public float Speed;
        public int Loot;
    }
}