using Codebase.Data.ScriptableObjects;
using Codebase.Enemy;

namespace Codebase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadData();
        EnemyStaticData ForMonster(EnemyTypeId id);
        LevelStaticData ForLevel(string sceneKey);
        PlayerStaticData ForPlayer();
    }
}