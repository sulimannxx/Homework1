using UnityEngine;

public class BackButtonOptions : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenuButtons;
    [SerializeField] private GameObject _pauseMenuButtons;
    [SerializeField] private AudioSource _tapAudioSource;

    public void OnButtonClick()
    {
        _tapAudioSource.Play();
        _optionsMenuButtons.SetActive(false);
        _pauseMenuButtons.SetActive(true);
    }
}
