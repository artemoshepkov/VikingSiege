using System;
using Codebase.Enemy;
using Codebase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Codebase.Data
{
    [Serializable]
    public class EnemySpawnerData
    {
        public EnemyTypeId EnemyTypeId;
        public Vector3 Position;

        public EnemySpawnerData(EnemyTypeId enemyTypeId, Vector3 position)
        {
            EnemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}