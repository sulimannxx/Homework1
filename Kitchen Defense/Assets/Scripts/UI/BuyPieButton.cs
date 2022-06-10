using UnityEngine;
using UnityEngine.UI;

public class BuyPieButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _pieAmount;
    [SerializeField] private int _piePrice;
    [SerializeField] private Image _image;
    [SerializeField] private AudioSource _audioSource;

    private Color _enoughMoneyColor = new Color(0.7311321f, 1, 0.7647856f, 1);
    private Color _notEnoughMoneyColor = new Color(0.990566f, 0.4251958f, 0.4703167f, 1);

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
            _image.color = _enoughMoneyColor;
        }
        else
        {
            _image.color = _notEnoughMoneyColor;
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