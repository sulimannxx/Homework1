using UnityEngine;
using UnityEngine.UI;

public class CloseWinWindow : MonoBehaviour
{
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _nextWaveButton;
    [SerializeField] private Image _nextWaveButtonImage;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private AudioSource _audioSource;

    public void ButtonPressed()
    {
        _audioSource.Play();
        _winWindow.SetActive(false);
        _waveController.StartAddAlphaValueToObjectCoroutine(_nextWaveButton, _nextWaveButtonImage);
    }
}
