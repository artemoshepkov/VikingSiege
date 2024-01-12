using Codebase.Enemy;
using Codebase.Infrastructure.Services.Factory;
using UnityEngine;

namespace Codebase.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        private int _lootValue;
        private IGameFactory _gameFactory;

        public EnemyDeath EnemyDeath;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        private void Start()
        {
            EnemyDeath.Died += SpawnLoot;
        }

        public void SetLootValue(int value)
        {
            _lootValue = value;
        }

        private void SpawnLoot()
        {
            LootPiece lootPiece = _gameFactory.CreateLoot();
            lootPiece.transform.position = transform.position;
            
            lootPiece.Initialize(_lootValue);
        }
    }
}