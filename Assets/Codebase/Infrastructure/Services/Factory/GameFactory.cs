using System.Collections.Generic;
using Codebase.Data.ScriptableObjects;
using Codebase.Enemy;
using Codebase.Infrastructure.Services.AssetManagment;
using Codebase.Infrastructure.Services.Progress;
using Codebase.Infrastructure.Services.Random;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic.ObjectsPool;
using Codebase.Logic.Stats;
using Codebase.Logic.Stats.StatsShop;
using Codebase.Logic.Waves;
using Codebase.Loot;
using Codebase.Player;
using Codebase.Player.Stats;
using Codebase.Spawner;
using Codebase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Codebase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        private GameObject _tower;
        private IStatsShop _statsShop;

        public GameFactory(IAsset assetProvider, IStaticDataService staticDataService, IRandomService randomService,
            IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _randomService = randomService;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public GameObject CreateTower(Vector3 at)
        {
            PlayerStaticData staticData = _staticDataService.ForPlayer();

            _tower = _assetProvider.Instantiate(AssetPath.TowerPath, at);

            TowerHealth health = _tower.GetComponent<TowerHealth>();
            health.Construct(staticData.MaxHealth, staticData.MaxHealthPriceUpgrade, staticData.MaxHealthUpgradeValue);

            RegenerationHealth regenHealth = _tower.GetComponent<RegenerationHealth>();
            regenHealth.Construct(staticData.RegenValue, staticData.RegenPriceUpgrade, staticData.RegenUpgradeValue);

            var defense = new Defense(health, staticData.DefenseValue, staticData.DefenseUpgradeValue, staticData.DefensePriceUpgrade);
            _tower.GetComponent<TakeTowerDamage>().Construct(defense);

            ShootSpeed shootSpeed = InitTowerShoot(staticData);

            TowerDamage towerDamage = _tower.GetComponent<TowerDamage>();
            towerDamage.Construct(staticData.DamageValue, staticData.DamagePriceUpgrade, staticData.DamageUpgradeValue);

            AttackRange attackRange = _tower.GetComponent<AttackRange>();
            attackRange.Construct(staticData.AttackRangeValue, staticData.AttackRangePriceUpgrade, staticData.AttackRangeUpgradeValue);

            InitStatsShop(health, regenHealth, defense, towerDamage, shootSpeed, attackRange);

            return _tower;
        }

        private ShootSpeed InitTowerShoot(PlayerStaticData staticData)
        {
            var shootSpeed = new ShootSpeed(staticData.ShootSpeedValue, staticData.ShootSpeedPriceUpgrade,
                staticData.ShootSpeedUpgradeValue);
            var missilesPool = new GameObjectsPool(CreateMissile, 5);

            TowerShoot shoot = _tower.GetComponent<TowerShoot>();
            shoot.Construct(shootSpeed, missilesPool);
            return shootSpeed;
        }

        private void InitStatsShop(TowerHealth health, RegenerationHealth regHealth, Defense defense,
            TowerDamage towerDamage, ShootSpeed shootSpeed, AttackRange attackRange)
        {
            ITowerStats towerStats = new TowerStats(
                new Dictionary<StatsId, IUpgradableStat>()
                {
                    {StatsId.Health, health},
                    {StatsId.Regeneration, regHealth},
                    {StatsId.Defence, defense},
                    {StatsId.Damage, towerDamage},
                    {StatsId.AttackSpeed, shootSpeed},
                    {StatsId.AttackRange, attackRange},
                });

            _statsShop = new StatsShop(towerStats, _progressService.PlayerProgress.WorldData.LootData);
        }

        public GameObject CreateEnemy(EnemyTypeId id, Transform parent)
        {
            EnemyStaticData staticData = _staticDataService.ForMonster(id);

            GameObject enemy = Object.Instantiate(staticData.Prefab, parent.position, Quaternion.identity, parent);

            enemy.GetComponent<EnemyDamage>().Construct(staticData.Damage);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.MaxHp = health.CurrentHp = staticData.HP;

            EnemyDeath death = enemy.GetComponent<EnemyDeath>();
            death.Construct(_progressService.PlayerProgress.WorldData.EnemiesData);

            enemy.GetComponent<EnemyMove>().Speed = staticData.Speed;

            InitLootSpawner(enemy, staticData);

            return enemy;
        }

        private void InitLootSpawner(GameObject enemy, EnemyStaticData staticData)
        {
            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this);
            lootSpawner.SetLootValue(staticData.Loot);
        }

        public EnemySpawnerPoint CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            GameObject spawner = _assetProvider.Instantiate(AssetPath.Spawner, at);

            EnemySpawnerPoint spawnerPoint = spawner.GetComponent<EnemySpawnerPoint>();

            PoolBase<GameObject> enemiesPool =
                new GameObjectsPool(() => CreateEnemy(enemyTypeId, spawnerPoint.transform), 5);

            spawnerPoint.Construct(enemiesPool);

            return spawnerPoint;
        }

        public WaveController CreateWaveController(List<EnemySpawnerPoint> spawners)
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);

            GameObject waveControllerGo = _assetProvider.Instantiate(AssetPath.WaveController);
            WaveController waveController = waveControllerGo.GetComponent<WaveController>();

            waveController.Construct(levelData.Waves, spawners, levelData.TimeBetweenWaves,
                levelData.TimeBetweenSpawnEnemies, _randomService);

            return waveController;
        }

        public GameObject CreateMissile()
        {
            Missile missile = _assetProvider.Instantiate(AssetPath.Missile).GetComponent<Missile>();

            missile.Construct(_tower.GetComponent<TowerDamage>());

            return missile.gameObject;
        }

        public LootPiece CreateLoot()
        {
            LootPiece loot = _assetProvider.Instantiate(AssetPath.Loot).GetComponent<LootPiece>();

            loot.Construct(_progressService.PlayerProgress.WorldData.LootData);

            return loot;
        }

        public GameObject CreateHUD()
        {
            GameObject hud = _assetProvider.Instantiate(AssetPath.HUD);

            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.PlayerProgress.WorldData.LootData);
            hud.GetComponentInChildren<KilledEnemiesCounter>()
                .Construct(_progressService.PlayerProgress.WorldData.EnemiesData);

            foreach (UpgradeStatButton button in hud.GetComponentsInChildren<UpgradeStatButton>())
            {
                button.Construct(_statsShop, _progressService.PlayerProgress.WorldData.LootData);
            }

            return hud;
        }
    }
}