using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
   [SerializeField] private Coin _coin;

    private float _spawnDelay = 2;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), _spawnDelay, _spawnDelay);
    }

    private void SpawnCoin()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}
