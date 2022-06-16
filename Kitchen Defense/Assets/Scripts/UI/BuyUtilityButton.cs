using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BuyUtilityButton : ShopButton
{
    [SerializeField] private Player _player;
    [SerializeField] private Utility _utility;
    [SerializeField] private int _utilityPrice;
    [SerializeField] private Image _image;
    [SerializeField] private AudioSource _audioSource;

    public UnityAction<int> PriceChanged;

    public int UtilityPrice => _utilityPrice;

    private void Start()
    {
        RecountIfPlayerHasEnoughMoney();
    }

    private void OnEnable()
    {
        RecountIfPlayerHasEnoughMoney();
    }

    public void BuyButton()
    {
        if (_player.Money >= _utilityPrice && _utility.IsBought == false)
        {
            _audioSource.Play();
            _utility.EnableUtility(true);
            _player.DecreaseMoney(_utilityPrice);
            RecountIfPlayerHasEnoughMoney();
        }
    }

    private void RecountIfPlayerHasEnoughMoney()
    {
        if (_utility.TryGetComponent(out SolidWall wall))
        {
            _utilityPrice = WaveController.GameWave * 2;
        }

        if (_utility.TryGetComponent(out Warehouse warehouse))
        {
            _utilityPrice = (int)_player.SpellBook.GetSkillPrice("WarehouseCapacitySkill");
            PriceChanged?.Invoke(_utilityPrice);
        }

        if (_player.Money >= _utilityPrice && _utility.IsBought == false)
        {
            _image.color = EnoughMoneyColor;
        }
        else
        {
            _image.color = NotEnoughMoneyColor;
        }
    }
}