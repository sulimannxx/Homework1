using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int _spawnersAmount = 3;
    private const int _spawnerDefaultIndex = 0;

    [SerializeField] private Sphere _sphere;

    private SpawnPoint[] _spawnPoints;
    private string _spawnMethodName = "SpawnSphere";
    private float _spawnTime = 2;
    private int _counter = _spawnerDefaultIndex;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        InvokeRepeating(_spawnMethodName, _spawnTime, _spawnTime);
    }

    private void SpawnSphere()
    {
        if (_counter > _spawnersAmount - 1)
        {
            _counter = _spawnerDefaultIndex;
        }

        Instantiate(_sphere, new Vector3(_spawnPoints[_counter].transform.position.x, _spawnPoints[_counter].transform.position.y, _spawnPoints[_counter].transform.position.z), Quaternion.identity);
        IncrementCounter();
    }

    private void IncrementCounter()
    {
        ++_counter;
    }
}
