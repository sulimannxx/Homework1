using UnityEngine;
using UnityEngine.UI;

public class BuyGloveButton : ShopButton
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

    public bool SkinIsBought { get; private set; } = false;

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
        if (SkinIsBought == false)
        {
            if (_player.Money >= _glovePriceInCoins && _player.PieCoins >= _glovePriceInPies)
            {
                _image.color = EnoughMoneyColor;
            }
            else
            {
                _image.color = NotEnoughMoneyColor;
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
        if (_player.Money >= _glovePriceInCoins && SkinIsBought == false)
        {
            _buyAudioSource.Play();
            SkinIsBought = true;
            _player.DecreaseMoney(_glovePriceInCoins);
            _player.AddGreyGloveBonusStats(_bonusDamagePercent, _bonusCritPercent);
            _player.SetPlayerSkin(_gloveSkin);
            Destroy(_moneyIcon.gameObject);
            Destroy(_moneyText.gameObject);
            _useText.SetActive(true);
            _image.color = EnoughMoneyColor;
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
            _progressSaveManager.PlayerProfile.BoughtSkinsId.Add(_skinId);
        }
        else if (SkinIsBought == true)
        {
            _useAudioSource.Play();
            _player.AddGreyGloveBonusStats(_bonusDamagePercent, _bonusCritPercent);
            _player.SetPlayerSkin(_gloveSkin);
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
    }

    public void BuyBlueGloveButton()
    {
        if (_player.Money >= _glovePriceInCoins && _player.PieCoins >= _glovePriceInPies && SkinIsBought == false)
        {
            _buyAudioSource.Play();
            SkinIsBought = true;
            _player.DecreaseMoneyAndPies(_glovePriceInCoins, _glovePriceInPies);
            _player.AddBlueGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease);
            _player.SetPlayerSkin(_gloveSkin);
            Destroy(_moneyIcon.gameObject);
            Destroy(_moneyText.gameObject);
            Destroy(_pieIcon.gameObject);
            Destroy(_pieText.gameObject);
            _useText.SetActive(true);
            RecountIfPlayerHasEnoughMoney();
            _image.color = EnoughMoneyColor;
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
            _progressSaveManager.PlayerProfile.BoughtSkinsId.Add(_skinId);
        }
        else if (SkinIsBought == true)
        {
            _useAudioSource.Play();
            _player.AddBlueGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease);
            _player.SetPlayerSkin(_gloveSkin);
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
    }

    public void BuyGoldenGloveButton()
    {
        if (_player.PieCoins >= _glovePriceInPies && SkinIsBought == false)
        {
            _buyAudioSource.Play();
            SkinIsBought = true;
            _player.DecreasePies(_glovePriceInPies);
            _player.AddGoldenGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease, _bonusPieChanceDrop);
            _player.SetPlayerSkin(_gloveSkin);
            Destroy(_moneyIcon.gameObject);
            Destroy(_moneyText.gameObject);
            _useText.SetActive(true);
            RecountIfPlayerHasEnoughMoney();
            _image.color = EnoughMoneyColor;
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
            _progressSaveManager.PlayerProfile.BoughtSkinsId.Add(_skinId);
        }
        else if (SkinIsBought == true)
        {
            _useAudioSource.Play();
            _player.AddGoldenGloveBonusStats(_bonusDamagePercent, _bonusCritPercent, _weaponCooldownDecrease, _bonusPieChanceDrop);
            _player.SetPlayerSkin(_gloveSkin);
            _progressSaveManager.PlayerProfile.CurrentSkinId = _skinId;
        }
    }

    public void LoadButtonState(int id)
    {
        if (id == _skinId)
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