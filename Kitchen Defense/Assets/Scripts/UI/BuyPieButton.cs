using UnityEngine;
using UnityEngine.UI;

public class BuyPieButton : ShopButton
{
    [SerializeField] private Player _player;
    [SerializeField] private int _pieAmount;
    [SerializeField] private int _piePrice;
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
        if (_player.Money >= _piePrice)
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
        if (_player.Money >= _piePrice)
        {
            _audioSource.Play();
            _player.DecreaseMoney(_piePrice);
            _player.AddPieFromShop(_pieAmount);
            RecountIfPlayerHasEnoughMoney();
        }
    }
}