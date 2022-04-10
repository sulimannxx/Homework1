using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
   [SerializeField] private GameObject _coin;

    private string _spawnMethodName = nameof(SpawnCoin);
    private float _spawnDelay = 2;

    private void Start()
    {
        InvokeRepeating(_spawnMethodName, _spawnDelay, _spawnDelay);
    }

    private void SpawnCoin()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}
