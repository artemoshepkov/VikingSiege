namespace Codebase.Logic.Stats
{
    public interface ITowerStats
    {
        IUpgradableStat GetStat(StatsId statIdType);
    }
}