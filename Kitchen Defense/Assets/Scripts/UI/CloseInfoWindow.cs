using UnityEngine;
using UnityEngine.Events;

public class CloseInfoWindow : MonoBehaviour
{
    [SerializeField] private GameObject _playerInfoScreen;
    [SerializeField] private AudioSource _audioSource;

    public UnityAction<bool> InfoButtonClosePressed;

    public void ButtonClick()
    {
        _audioSource.Play();
        _playerInfoScreen.SetActive(false);
        InfoButtonClosePressed?.Invoke(false);
    }
}
