using UnityEngine;

public class PlayerInfoButton : MonoBehaviour
{
    [SerializeField] private GameObject _playerInfoScreen;
    [SerializeField] private ShopElement _shop;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerHealText;

    public void ButtonClick()
    {
        _audioSource.Play();
        _playerInfoScreen.SetActive(true);
        _shop.gameObject.SetActive(false);
        _player.OnUiButtonPressed(true);
        _playerHealText.SetActive(false);
    }
}
