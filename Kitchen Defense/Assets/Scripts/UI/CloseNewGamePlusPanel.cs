using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseNewGamePlusPanel : MonoBehaviour
{
    [SerializeField] private GameObject _newGamePlusPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioSource _tapAudioSource;

    public void OnButtonClick()
    {
        _tapAudioSource.Play();
        _newGamePlusPanel.SetActive(false);
        _pausePanel.SetActive(true);
    }
}
