using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Image _blackFilter;
    [SerializeField] private AudioSource _tapAudioSource;
    [SerializeField] private AudioSource[] _backgroundMusic;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _playerInfo;
    [SerializeField] private GameObject _newGamePlusPanel;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerHealText;

    public void PressPauseButton()
    {
        if (_blackFilter.IsActive() == true)
        {
            foreach (var backgroundMusic in _backgroundMusic)
            {
                backgroundMusic.UnPause();
            }

            _blackFilter.gameObject.SetActive(false);
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
            _newGamePlusPanel.SetActive(false);
            _player.OnUiButtonPressed(false);
        }
        else if (_blackFilter.IsActive() == false)
        {
            foreach (var backgroundMusic in _backgroundMusic)
            {
                backgroundMusic.Pause();
            }

            _shop.SetActive(false);
            _playerInfo.SetActive(false);
            Time.timeScale = 0;
            _blackFilter.gameObject.SetActive(true);
            _pausePanel.SetActive(true);
            _playerHealText.SetActive(false);
        }

        _tapAudioSource.Play();
    }
}
