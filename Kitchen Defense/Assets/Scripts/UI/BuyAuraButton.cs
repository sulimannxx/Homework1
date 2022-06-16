using UnityEngine;
using UnityEngine.UI;

public class BuyAuraButton : ShopButton
{
    [SerializeField] private Player _player;
    [SerializeField] private int _auraActiveSeconds;
    [SerializeField] private int _auraItemPrice;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _auraSprite;
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
        if (_player.PieCoins >= _auraItemPrice && _player.AuraIsActivated == false)
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
        if (_player.PieCoins >= _auraItemPrice && _player.AuraIsActivated == false)
        {
            _audioSource.Play();
            _player.DecreasePies(_auraItemPrice);
            _player.ActivateAura(_auraActiveSeconds, _auraSprite);
            RecountIfPlayerHasEnoughMoney();
        }
    }
}