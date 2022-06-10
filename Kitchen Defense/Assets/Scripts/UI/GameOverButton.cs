using UnityEngine;
using UnityEngine.Events;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] private Wave _wave;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private WaveCompletedPanel _gameOverPanel;
    [SerializeField] private GameObject _repeatWaveButton;
    [SerializeField] private GameObject _previousWaveButton;
    [SerializeField] private AudioSource _audioSource;

    public event UnityAction<bool> WaveUiButtonPressed;

    public void RepeatWave()
    {
        _audioSource.Play();
        _wave.RecalculateNextWaveStrength();
        _spawner.ResetSpawnedEnemies();
        _player.ResetEarnedOnThisWaveValue();
        _player.ResetHealthAndEnablePlayer();
        WaveUiButtonPressed?.Invoke(false);
        _repeatWaveButton.SetActive(false);
        _previousWaveButton.SetActive(false);
        _gameOverPanel.gameObject.SetActive(false);
        Camera.main.GetComponent<ProgressSaveManager>().SaveGame();
    }

    public void RepeatPreviousWave()
    {
        WaveController.GameWave--;

        if (WaveController.GameWave == 0)
        {
            WaveController.GameWave = 1;
        }

        Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.GameWave = WaveController.GameWave;
        _audioSource.Play();
        _wave.RecalculateNextWaveStrength();
        _spawner.ResetSpawnedEnemies();
        _player.ResetEarnedOnThisWaveValue();
        _player.ResetHealthAndEnablePlayer();
        WaveUiButtonPressed?.Invoke(false);
        _repeatWaveButton.SetActive(false);
        _previousWaveButton.SetActive(false);
        _gameOverPanel.gameObject.SetActive(false);
        Camera.main.GetComponent<ProgressSaveManager>().SaveGame();
    }
}