using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Amount <= _spawned)
        {
            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, Quaternion.identity, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.EnemyIsDead += OnEnemyDead;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.EnemyIsDead -= OnEnemyDead;
        _player.AddMoney(enemy.GetReward());
    }
}

[System.Serializable]

public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Amount;
}
