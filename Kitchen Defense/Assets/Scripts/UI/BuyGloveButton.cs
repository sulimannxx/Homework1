using UnityEngine;
using UnityEngine.UI;

public class BuyGloveButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Sprite _gloveSkin;
    [SerializeField] private int _glovePriceInCoins;
    [SerializeField] private int _glovePriceInPies;
    [SerializeField] private float _bonusDamagePercent;
    [SerializeField] private float _bonusCritPercent;
    [SerializeField] private float _weaponCooldownDecrease;
    [SerializeField] private float _bonusPieChanceDrop;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _moneyIcon;
    [SerializeField] private GameObject _pieIcon;
    [SerializeField] private GameObject _moneyText;
    [SerializeField] private GameObject _pieText;
    [SerializeField] private GameObject _useText;
    [SerializeField] private AudioSource _buyAudioSource;
    [SerializeField] private AudioSource _useAudioSource;
    [SerializeField] private ProgressSaveManager _progressSaveManager;
    [SerializeField] private int _skinId;

    private bool _skinIsBought = false;
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

    public void RecountIfPlayerHasEnoughMoney()
    {
        if (_skinIsBought == false)
        {
            if (_player.Money >= _glovePriceInCoins && _player.PieCoins >= _glovePriceInPies)
            {
                _image.color = _enoughMoneyColor;
            }
            else
            {
                _image.color = _notEnoughMoneyColor;
            }
        }
    }

    public void UseStandartGlove()
    {
        _useAudioSource.Play();
        _player.SetBonusStatsDefaultValues();
        _player.SetPlayerSkin(_gloveSkin);
        _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
    }

    public void BuyGreyGloveButton()
    {
        if (_player.Money >= _glovePriceInCoins && _skinIsBought == false)
        {
            _buyAudioSource.Play();
            _skinIsBought = true;
            _player.DecreaseMoney(_glovePriceInCoins);
            _player.AddGreyGloveBonusStats(_bonusDamagePercent, _bonusCritPercent);
            _player.SetPlayerSkin(_gloveSkin);
            Destroy(_moneyIcon.gameObject);
            Destroy(_moneyText.gameObject);
            _useText.SetActive(true);
            _image.color = _enoughMoneyColor;
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
        else if (_skinIsBought == true)
        {
            _useAudioSource.Play();
            _player.AddGreyGloveBonusStats(_bonusDamagePercent, _bonusCritPercent);
            _player.SetPlayerSkin(_gloveSkin);
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
    }

    public void BuyBlueGloveButton()
    {
        if (_player.Money >= _glovePriceInCoins && _player.PieCoins >= _glovePriceInPies && _skinIsBought == false)
        {
            _buyAudioSource.Play();
            _skinIsBought = true;
            _player.DecreaseMoneyAndPies(_glovePriceInCoins, _glovePriceInPies);
            _player.AddBlueGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease);
            _player.SetPlayerSkin(_gloveSkin);
            Destroy(_moneyIcon.gameObject);
            Destroy(_moneyText.gameObject);
            Destroy(_pieIcon.gameObject);
            Destroy(_pieText.gameObject);
            _useText.SetActive(true);
            RecountIfPlayerHasEnoughMoney();
            _image.color = _enoughMoneyColor;
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
        else if (_skinIsBought == true)
        {
            _useAudioSource.Play();
            _player.AddBlueGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease);
            _player.SetPlayerSkin(_gloveSkin);
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
    }

    public void BuyGoldenGloveButton()
    {
        if (_player.PieCoins >= _glovePriceInPies && _skinIsBought == false)
        {
            _buyAudioSource.Play();
            _skinIsBought = true;
            _player.DecreasePies(_glovePriceInPies);
            _player.AddGoldenGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease, _bonusPieChanceDrop);
            _player.SetPlayerSkin(_gloveSkin);
            Destroy(_moneyIcon.gameObject);
            Destroy(_moneyText.gameObject);
            _useText.SetActive(true);
            RecountIfPlayerHasEnoughMoney();
            _image.color = _enoughMoneyColor;
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
        else if (_skinIsBought == true)
        {
            _useAudioSource.Play();
            _player.AddGoldenGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease, _bonusPieChanceDrop);
            _player.SetPlayerSkin(_gloveSkin);
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
    }

    public void LoadButtonState(bool state)
    {
        //������� ���� ��������
        if (state == true)
        {
            if (_moneyIcon != null && _moneyText != null)
            {
                Destroy(_moneyIcon.gameObject);
                Destroy(_moneyText.gameObject);
            }

            if (_pieIcon != null && _pieText != null)
            {
                Destroy(_pieIcon.gameObject);
                Destroy(_pieText.gameObject);
            }

            _useText.SetActive(true);
        }
    }
}