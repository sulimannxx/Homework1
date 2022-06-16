using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BuyWeaponButton : ShopButton
{
    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _weaponPrice;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ProgressSaveManager _progressSaveManager;
    [SerializeField] private int _weaponId;

    public int WeaponPrice => _weaponPrice;

    private void Start()
    {
        _weapon.Bought(false);
        RecountIfPlayerHasEnoughMoney();
    }

    private void OnEnable()
    {
        RecountIfPlayerHasEnoughMoney();
    }

    public void BuyButton()
    {
        if (_player.Money >= _weaponPrice && _weapon.IsBought == false)
        {
            _audioSource.Play();
            _player.AddWeaponToInventory(_weapon, _weaponPrice);
            _weapon.Bought(true);
            this.gameObject.SetActive(false);
            _upgradeButton.SetActive(true);
            _progressSaveManager.PlayerProfile.BoughtWeaponsId.Add(_weaponId);
            RecountIfPlayerHasEnoughMoney();
        }
    }

    public void RecountIfPlayerHasEnoughMoney()
    {
        if (_player.Money >= _weaponPrice && _weapon.IsBought == false)
        {
            _image.color = EnoughMoneyColor;
        }
        else
        {
            _image.color = NotEnoughMoneyColor;
        }
    }

    public void LoadButtonState(int id)
    {
        if (id == _weaponId)
        {
            this.gameObject.SetActive(false);
            _upgradeButton.SetActive(true);
            _player.LoadWeaponInventory(_weapon);
        }
    }
}
