using System;
using System.Collections.Generic;
using Codebase.Data;
using Codebase.Data.ScriptableObjects;
using Codebase.Infrastructure.Services.Factory;
using Codebase.Infrastructure.Services.StaticData;
using Codebase.Logic;
using Codebase.Player;
using Codebase.Spawner;
using Codebase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Codebase.Infrastructure.States
{
    public class LevelLoadState : IPayloadState<string>
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private IStaticDataService _staticData;

        public LevelLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IStaticDataService staticData)
        {
            _staticData = staticData;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string levelName)
        {
            // _loadingCurtain.Show();
            _sceneLoader.Load(levelName, OnLoaded);
        }

        public void Exit()
        {
            // _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            List<EnemySpawnerPoint> spawners = InitSpawners();

            InitWaveController(spawners);
            
            GameObject tower =
                _gameFactory.CreateTower(GameObject.FindWithTag(PlayerInitialPoint).transform.position);

            InitHUD(tower);
        }

        private void InitWaveController(List<EnemySpawnerPoint> spawners) => _gameFactory.CreateWaveController(spawners);

        private void InitHUD(GameObject tower)
        {
            GameObject hud = _gameFactory.CreateHUD();

            hud.GetComponent<
                ActorUI>().Construct(tower.GetComponent<IHealth>());
        }

        private List<EnemySpawnerPoint> InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            var spawners = new List<EnemySpawnerPoint>();
            foreach (EnemySpawnerData enemySpawner in levelData.EnemySpawners)
            {
                spawners.Add(_gameFactory.CreateSpawner(enemySpawner.Position, enemySpawner.EnemyTypeId));
            }

            return spawners;
        }

        private static GameObject Instantiate(string path)
        {
            Object prefab = Resources.Load(path);

            return (GameObject) Object.Instantiate(prefab);
        }
    }
}