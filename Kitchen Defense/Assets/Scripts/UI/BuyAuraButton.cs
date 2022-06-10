using UnityEngine;
using UnityEngine.UI;

public class BuyAuraButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _auraActiveSeconds;
    [SerializeField] private int _auraItemPrice;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _auraSprite;
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
        if (_player.PieCoins >= _auraItemPrice && _player.AuraIsActivated == false)
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
        if (_player.PieCoins >= _auraItemPrice && _player.AuraIsActivated == false)
        {
            _audioSource.Play();
            _player.DecreasePies(_auraItemPrice);
            _player.ActivateAura(_auraActiveSeconds, _auraSprite);
            RecountIfPlayerHasEnoughMoney();
        }
    }
}