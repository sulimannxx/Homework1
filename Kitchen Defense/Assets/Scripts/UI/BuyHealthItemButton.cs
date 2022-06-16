using UnityEngine;
using UnityEngine.UI;

public class BuyHealthItemButton : ShopButton
{
    [SerializeField] private Player _player;
    [SerializeField] private int _healAmount;
    [SerializeField] private int _healItemPrice;
    [SerializeField] private Image _image;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        RecountIfPlayerHasEnoughMoney();
    }
    private void OnEnable()
    {
        RecountIfPlayerHasEnoughMoney();
    }

    private void RecountIfPlayerHasEnoughMoney()
    {
        if (_player.Money >= _healItemPrice)
        {
            _image.color = EnoughMoneyColor;
        }
        else
        {
            _image.color = NotEnoughMoneyColor;
        }
    }

    public void BuyButton()
    {
        if (_player.Money >= _healItemPrice)
        {
            _audioSource.Play();
            _player.DecreaseMoney(_healItemPrice);
            _player.HealByItem(_healAmount);
            RecountIfPlayerHasEnoughMoney();
        }
    }
}
