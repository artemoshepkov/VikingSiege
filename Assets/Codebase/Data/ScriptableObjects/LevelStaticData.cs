using System.Collections.Generic;
using Codebase.Logic.Waves;
using UnityEngine;

namespace Codebase.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string SceneKey;
        public List<Wave> Waves;
        public float TimeBetweenWaves;
        public Vector2 TimeBetweenSpawnEnemies;
        
        public List<EnemySpawnerData> EnemySpawners;
    }
}