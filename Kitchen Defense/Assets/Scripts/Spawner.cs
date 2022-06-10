using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private Wave _currentWave;

    private List<GameObject> _spawnedEnemies;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Start()
    {
        _player.ResetEarnedOnThisWaveValue();
        _currentWave.DeadEnemies = _currentWave.Amount;
        _spawnedEnemies = new List<GameObject>();
    }

    private void Update()
    {
        _timeAfterLastSpawn += Time.deltaTime;
        if (_timeAfterLastSpawn >= _currentWave.Delay && _spawned < _currentWave.Amount)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template[Random.Range(0, _currentWave.Template.Length)], _spawnPoint.position, Quaternion.identity, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.EnemyIsDead += OnEnemyDead;
        _spawnedEnemies.Add(enemy.gameObject);
        _currentWave.RecalculateRandomSpawnTime();
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.EnemyIsDead -= OnEnemyDead;
        _player.AddMoney(enemy.GetReward());
        _currentWave.DeadEnemies--;

        if (_currentWave.DeadEnemies <= 0)
        {
            _waveController.EnableWinScreen();
        }
    }

    public void ResetSpawnedEnemies()
    {
        _spawned = 0;
        DestroyAllEnemiesOnReset();
    }

    public void DestroyAllEnemiesOnReset()
    {
        foreach (var enemy in _spawnedEnemies)
        {
            Destroy(enemy);
        }
    }
}

