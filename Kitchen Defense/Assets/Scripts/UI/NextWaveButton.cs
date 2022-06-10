using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Wave _wave;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private WaveCompletedPanel _waveCompletedPanel;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ProgressSaveManager _progressSaveManager;
    [SerializeField] private SkillBook _skillBook;

    private Image _buttonImage;
    private float _currentTimeValue;
    private float _maxTimeValue = 5;

    public event UnityAction<bool> WaveUiButtonPressed;

    private void OnEnable()
    {
        _buttonImage = GetComponent<Image>();
        StartCoroutine(NextWaveCountDown());
    }

    public void OnButtonClick()
    {
        _audioSource.Play();
        WaveController.GameWave++;
        _progressSaveManager.PlayerProfile.GameWave = WaveController.GameWave;
        _wave.RecalculateNextWaveStrength();
        _spawner.ResetSpawnedEnemies();
        _player.ResetEarnedOnThisWaveValue();
        WaveUiButtonPressed?.Invoke(false);
        gameObject.SetActive(false);
        _waveCompletedPanel.gameObject.SetActive(false);
        _skillBook.SaveSkillLevels();
        _progressSaveManager.SaveGame();
    }

    private IEnumerator NextWaveCountDown()
    {
        _buttonImage.fillAmount = 1;
        _currentTimeValue = _maxTimeValue;

        while (_buttonImage.fillAmount > 0)
        {
            _currentTimeValue -= Time.unscaledDeltaTime;
            _buttonImage.fillAmount = _currentTimeValue / _maxTimeValue;
            yield return null;

            if (_buttonImage.fillAmount == 0)
            {
                OnButtonClick();
            }
        }
    }
}
