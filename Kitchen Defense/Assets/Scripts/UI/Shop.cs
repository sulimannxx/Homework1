using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopElement _shop;
    [SerializeField] private Player _player;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private List<BuyWeaponButton> _buyWeaponButtons;

    private void Start()
    {
        _shop.gameObject.SetActive(false);
    }

    public void ActivateShop()
    {
        if (_shop.gameObject.activeSelf == true)
        {
            Time.timeScale = 1;
            _shop.gameObject.SetActive(false);
        }
        else if (_shop.gameObject.activeSelf == false)
        {
            Time.timeScale = 0;
            _shop.gameObject.SetActive(true);

            foreach (var buyWeaponButton in _buyWeaponButtons)
            {
                buyWeaponButton.RecountIfPlayerHasEnoughMoney();
            }
        }      
    }
}
