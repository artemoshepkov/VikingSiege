using System;

namespace Codebase.Data
{
    [Serializable]
    public class WorldData
    {
        public LootData LootData;
        public EnemiesData EnemiesData;
        
        public WorldData()
        {
            LootData = new LootData();
            EnemiesData = new EnemiesData();
        }
    }
}