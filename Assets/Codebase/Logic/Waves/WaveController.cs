using System.Collections;
using System.Collections.Generic;
using Codebase.Infrastructure.Services.Random;
using Codebase.Spawner;
using UnityEngine;

namespace Codebase.Logic.Waves
{
    public class WaveController : MonoBehaviour
    {
        private int _currentWaveIndex = 0;
        private int _spawnedEnemies = 0;

        private List<Wave> _waves;
        private List<EnemySpawnerPoint> _spawners;
        private IRandomService _randomService;
        private float _timeBetweenWaves;
        private Vector2 _timeBetweenSpawn;

        public Wave CurrentWave => _waves[_currentWaveIndex];

        public void Construct(List<Wave> waves, List<EnemySpawnerPoint> spawners, float timeBetweenWaves,
            Vector2 timeBetweenSpawn,
            IRandomService randomService)
        {
            _timeBetweenSpawn = timeBetweenSpawn;
            _timeBetweenWaves = timeBetweenWaves;
            _randomService = randomService;
            _spawners = spawners;
            _waves = waves;
        }

        private void Start()
        {
            Activate();
        }

        public void Activate()
        {
            StartCoroutine(ActivateWave());
        }

        private IEnumerator ActivateWave()
        {
            while (!WaveIsEnd())
            {
                SpawnEnemy();
                float timeBetweenSpawn = _randomService.Range(_timeBetweenSpawn.x, _timeBetweenSpawn.y);
                yield return new WaitForSeconds(timeBetweenSpawn);
            }

            NextWave();
        }

        private bool WaveIsEnd()
        {
            return _spawnedEnemies >= _waves[_currentWaveIndex].AmountEnemies;
        }

        private void SpawnEnemy()
        {
            _spawnedEnemies++;
            int randomIndex = _randomService.Range(0, _spawners.Count);
            _spawners[randomIndex].Spawn();
        }

        public void NextWave()
        {
            _currentWaveIndex++;
            _spawnedEnemies = 0;
            if (_currentWaveIndex >= _waves.Count)
            {
                Debug.Log("Waves is ended");
                return;
            }

            Invoke(nameof(Activate), _timeBetweenWaves);
        }
    }
}