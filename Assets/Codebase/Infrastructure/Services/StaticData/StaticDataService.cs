using System.Collections.Generic;
using System.Linq;
using Codebase.Data.ScriptableObjects;
using Codebase.Enemy;
using UnityEngine;

namespace Codebase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataPlayerPath = "StaticData/Player/PlayerStaticData";

        private Dictionary<EnemyTypeId, EnemyStaticData> _monsters = new Dictionary<EnemyTypeId, EnemyStaticData>();
        private Dictionary<string, LevelStaticData> _levels = new Dictionary<string, LevelStaticData>();
        private PlayerStaticData _playerStaticData;

        public void LoadData()
        {
            _monsters = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(e => e.EnemyTypeId, e => e);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(e => e.SceneKey, e => e);

            _playerStaticData = Resources.Load<PlayerStaticData>(StaticDataPlayerPath);
        }

        public EnemyStaticData ForMonster(EnemyTypeId id) =>
            _monsters.TryGetValue(id, out EnemyStaticData data)
                ? data
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData data)
                ? data
                : null;

        public PlayerStaticData ForPlayer() => _playerStaticData;
    }
}