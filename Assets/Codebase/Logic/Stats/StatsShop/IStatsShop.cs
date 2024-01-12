namespace Codebase.Logic.Stats.StatsShop
{
    public interface IStatsShop
    {
        bool TryUpgrade(StatsId statId);
        bool CanUpgrade(IStat stat);
        IStat GetStatInfo(StatsId statId);
    }
}