using Codebase.Data;

namespace Codebase.Logic.Stats.StatsShop
{
    public class StatsShop : IStatsShop
    {
        private LootData _lootData;

        private readonly ITowerStats _towerStats;

        public StatsShop(ITowerStats towerStats, LootData lootData)
        {
            _lootData = lootData;
            _towerStats = towerStats;
        }

        public IStat GetStatInfo(StatsId statId) => _towerStats.GetStat(statId);

        public bool TryUpgrade(StatsId statId)
        {
            IUpgradableStat stat = _towerStats.GetStat(statId);
            
            if (!CanUpgrade(stat))
            {
                return false;
            }

            stat.Upgrade();
            _lootData.Spend(stat.Price);
            
            return true;
        }

        public bool CanUpgrade(IStat stat) => stat.Price <= _lootData.Balance;
    }
}