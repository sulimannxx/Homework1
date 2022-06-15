using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuButtons;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private AudioSource _tapAudioSource;

    public void OnButtonClick()
    {
        _tapAudioSource.Play();
        _pauseMenuButtons.SetActive(false);
        _optionsMenu.SetActive(true);
    }
}
