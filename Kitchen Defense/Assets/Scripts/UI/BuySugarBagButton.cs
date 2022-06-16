using UnityEngine;
using UnityEngine.UI;

public class BuySugarBagButton : ShopButton
{
    [SerializeField] private Player _player;
    [SerializeField] private int _moneyMinRange;
    [SerializeField] private int _moneyMaxRange;
    [SerializeField] private int _sugarBagPrice;
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
        if (_player.PieCoins >= _sugarBagPrice)
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
        if (_player.PieCoins >= _sugarBagPrice)
        {
            _audioSource.Play();
            int money = Random.Range(_moneyMinRange, _moneyMaxRange + 1);
            _player.DecreasePies(_sugarBagPrice);
            _player.AddMoneyFromShop(money);
            RecountIfPlayerHasEnoughMoney();
        }
    }
}