using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BuyWeaponButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _weaponPrice;
    [SerializeField] private Image _image;

    private Color _enoughMoneyColor = new Color(0.7311321f, 1, 0.7647856f, 1);
    private Color _notEnoughMoneyColor = new Color(0.990566f, 0.4251958f, 0.4703167f, 1);

    public int WeaponPrice => _weaponPrice;

    private void Start()
    {
        _weapon.Bought(false);
        RecountIfPlayerHasEnoughMoney();
    }

    public void BuyButton()
    {
        if (_player.Money >= _weaponPrice && _weapon.IsBought == false)
        {
            _player.AddWeaponToInventory(_weapon, _weaponPrice);
            _weapon.Bought(true);
        }       
    }

    public void RecountIfPlayerHasEnoughMoney()
    {
        if (_player.Money >= _weaponPrice && _weapon.IsBought == false)
        {
            _image.color = _enoughMoneyColor;
        }
        else
        {
            _image.color = _notEnoughMoneyColor;
        }
    }
}
