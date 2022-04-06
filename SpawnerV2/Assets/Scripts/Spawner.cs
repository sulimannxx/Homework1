using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int _spawnerDefaultIndex = 0;

    [SerializeField] private Sphere _sphere;

    private SpawnPoint[] _spawnPoints;
    private string _spawnMethodName = nameof(SpawnSphere);
    private float _spawnDelay = 2;
    private int _counter = _spawnerDefaultIndex;
    private int _spawnPointsAmount => _spawnPoints.Length;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        InvokeRepeating(_spawnMethodName, _spawnDelay, _spawnDelay);
    }

    private void SpawnSphere()
    {
        if (_counter > _spawnPointsAmount - 1)
        {
            _counter = _spawnerDefaultIndex;
        }

        Instantiate(_sphere, _spawnPoints[_counter].transform.position, Quaternion.identity);
        IncrementCounter();
    }

    private void IncrementCounter()
    {
        ++_counter;
    }
}
