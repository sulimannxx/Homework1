using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopElement _shop;
    [SerializeField] private Player _player;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private List<BuyWeaponButton> _buyWeaponButtons;
    [SerializeField] private GameObject _playerInfoScreen;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _playerHealText;

    public event UnityAction<bool> ShopUiButtonPressed;

    private void Awake()
    {
        _shop.gameObject.SetActive(false);
    }

    public void ActivateShop()
    {
        if (_shop.gameObject.activeSelf == true)
        {
            ActivateShopButtonPressedEvent(false);
        }
        else if (_shop.gameObject.activeSelf == false)
        {
            ActivateShopButtonPressedEvent(true);

            foreach (var buyWeaponButton in _buyWeaponButtons)
            {
                buyWeaponButton.RecountIfPlayerHasEnoughMoney();
            }
        }
        _audioSource.Play();
    }

    public void ActivateShopButtonPressedEvent(bool state)
    {
        if (state == false)
        {
            ShopUiButtonPressed?.Invoke(false);
            _shop.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if (state == true)
        {
            ShopUiButtonPressed?.Invoke(true);
            _playerInfoScreen.SetActive(false);
            Time.timeScale = 0;
            _shop.gameObject.SetActive(true);
            _playerHealText.SetActive(false);
        }
    }
}
