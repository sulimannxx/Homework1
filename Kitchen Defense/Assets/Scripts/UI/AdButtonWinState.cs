using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AdButtonWinState : MonoBehaviour
{
    [SerializeField] private Image _adImage;
    [SerializeField] private GameObject _adText;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private Player _player;

    private float _currentTimeValue;
    private float _maxTimeValue = 7;

    private void OnEnable()
    {
        StartCoroutine(AdTimerCountDown());
    }

    private IEnumerator AdTimerCountDown()
    {
        _adImage.fillAmount = 1;
        _currentTimeValue = _maxTimeValue;

        while (_adImage.fillAmount > 0)
        {
            _currentTimeValue -= Time.unscaledDeltaTime;
            _adImage.fillAmount = _currentTimeValue / _maxTimeValue;
            yield return null;

            if (_adImage.fillAmount == 0)
            {
                _adImage.gameObject.SetActive(false);
                _adText.SetActive(false);
                gameObject.SetActive(false);

                if (_player.CurrentHealth > 0)
                {
                    _waveController.EnableNextWaveButton();
                }
                else if (_player.CurrentHealth <= 0)
                {
                    _waveController.EnableGameOverButtons();
                }
            }
        }
    }

    public void ButtonPressed()
    {
        _player.DoubleEarnedMoneyOnWave(_waveController.EarnedSugarOnThisWave);
        _adImage.gameObject.SetActive(false);
        _adText.SetActive(false);
        gameObject.SetActive(false);

        if (_player.CurrentHealth > 0)
        {
            _waveController.EnableNextWaveButton();
        }
        else if (_player.CurrentHealth <= 0)
        {
            _waveController.EnableGameOverButtons();
        }
    }

}
