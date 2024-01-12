namespace Codebase.Logic.Stats
{
    public interface IUpgradableStat : IStat
    {
        int Price { get; }
        object Value { get; }
        
        void Upgrade();
    }
}