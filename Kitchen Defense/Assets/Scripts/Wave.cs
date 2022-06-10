using UnityEngine;
public class Wave : MonoBehaviour
{
    [SerializeField] public GameObject[] Template;

    public int DeadEnemies;

    private int _baseAmount = 5;
    private int _currentWaveNumber;
    public float Delay { get; private set; }
    public int Amount { get; private set; }
    private void Start()
    {
        RecalculateNextWaveStrength();
    }

    public void RecalculateNextWaveStrength()
    {
        _currentWaveNumber = WaveController.GameWave;
        RecalculateRandomSpawnTime();
        Amount = (int)(_baseAmount + _currentWaveNumber * 0.05f);
        DeadEnemies = Amount;
    }

    public void RecalculateRandomSpawnTime()
    {
        Delay = 3 - Random.Range(0, _currentWaveNumber / 100);

        if (Delay <= 1)
        {
            Delay = 1;
        }
    }
}