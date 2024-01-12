using System;

namespace Codebase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public Stats Stats;
        
        public PlayerProgress()
        {
            WorldData = new WorldData();
        }
    }
}