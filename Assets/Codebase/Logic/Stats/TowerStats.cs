using System.Collections.Generic;

namespace Codebase.Logic.Stats
{
    public class TowerStats : ITowerStats
    {
        private readonly Dictionary<StatsId, IUpgradableStat> _stats;
        
        public TowerStats(Dictionary<StatsId, IUpgradableStat> stats) => _stats = stats;

        public IUpgradableStat GetStat(StatsId statIdType) => _stats[statIdType];
    }
}