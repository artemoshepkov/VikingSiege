using System.Collections.Generic;
using Codebase.Enemy;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;
using Codebase.Logic.Waves;
using Codebase.Loot;
using Codebase.Player;
using Codebase.Spawner;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateTower(Vector3 at);
        GameObject CreateEnemy(EnemyTypeId id, Transform parent);
        EnemySpawnerPoint CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId);
        LootPiece CreateLoot();
        GameObject CreateHUD();
        WaveController CreateWaveController(List<EnemySpawnerPoint> spawners);
        GameObject CreateMissile();
    }
}