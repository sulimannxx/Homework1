using UnityEngine;

public class NewGamePlusButton : MonoBehaviour
{
    [SerializeField] private GameObject _newGamePlusPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioSource _tapAudioSource;

    public void StartNewGamePlusButton()
    {
        _tapAudioSource.Play();
        _pausePanel.SetActive(false);
        _newGamePlusPanel.SetActive(true); 
    }
}