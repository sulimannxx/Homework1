using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BuyUtilityButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Utility _utility;
    [SerializeField] private int _utilityPrice;
    [SerializeField] private Image _image;
    [SerializeField] private AudioSource _audioSource;

    private Color _enoughMoneyColor = new Color(0.7311321f, 1, 0.7647856f, 1);
    private Color _notEnoughMoneyColor = new Color(0.990566f, 0.4251958f, 0.4703167f, 1);

    public int UtilityPrice => _utilityPrice;

    private void Start()
    {
       // _utility.Bought(false);
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
            _utility.Bought(true);
            _player.DecreaseMoney(_utilityPrice);
            RecountIfPlayerHasEnoughMoney();
        }
    }

    private void RecountIfPlayerHasEnoughMoney()
    {
        if (_player.Money >= _utilityPrice && _utility.IsBought == false)
        {
            _image.color = _enoughMoneyColor;
        }
        else
        {
            _image.color = _notEnoughMoneyColor;
        }
    }
}