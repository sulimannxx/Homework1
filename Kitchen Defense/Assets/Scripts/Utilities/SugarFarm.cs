using System.Collections;
using UnityEngine;

public class SugarFarm : Utility
{
    [SerializeField] private Player _player;

    private float _minRandomTimeValue = 50f;
    private float _maxRandomTimeValue = 70f;
    private void Start()
    {
        if (Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.SugarFarmIsBought == true)
        {
            EnableFarm();
            IsBought = true;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SugarFarmDelay());
        Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.SugarFarmIsBought = true;
    }

    private IEnumerator SugarFarmDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minRandomTimeValue, _maxRandomTimeValue));

        if (_player.isActiveAndEnabled)
        {
            _player.AddMoney(10 * WaveController.GameWave);
        }

        StartCoroutine(SugarFarmDelay());
    }

    private void EnableFarm()
    {
        this.gameObject.SetActive(true);
    }

    public override bool Bought(bool state)
    {
        if (state == true)
        {
            EnableFarm();
        }

        return IsBought = state;
    }

    public override void Init()
    {
        Start();
    }
}
