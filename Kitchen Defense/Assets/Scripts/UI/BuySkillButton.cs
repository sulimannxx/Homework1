using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]

public class BuySkillButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SkillBook _skillBook;
    [SerializeField] private Image _image;
    [SerializeField] private string _skillName;
    [SerializeField] private WeaponInfoButton _weaponInfoButton; 
    [SerializeField] private AudioSource _audioSource;

    private int _skillPrice;
    private Color _enoughMoneyColor = new Color(0.7311321f, 1, 0.7647856f, 1);
    private Color _notEnoughMoneyColor = new Color(0.990566f, 0.4251958f, 0.4703167f, 1);

    public UnityAction PriceChanged;

    public int SkillPrice => _skillPrice;

    private void Start()
    {
        RecountIfPlayerHasEnoughMoney();
        GetSkillPrice();
        _weaponInfoButton.InfoButtonPressed += OnInfoButtonPressed;
    }

    private void OnEnable()
    {
        RecountIfPlayerHasEnoughMoney();
        GetSkillPrice();
    }

    private void OnInfoButtonPressed(bool state)
    {
        gameObject.SetActive(state);
    }

    private void GetSkillPrice()
    {
        _skillBook.RecountSkillPrice(_skillName);
        _skillPrice = (int) _skillBook.GetSkillPrice(_skillName);
    }

    public void BuyButton()
    {
        if (_player.Money >= _skillBook.GetSkillPrice(_skillName) && _skillBook.GetSkillLevel(_skillName) <_skillBook.GetSkillMaxLevel(_skillName))
        {
            _audioSource.Play();
            _player.DecreaseMoney((int)_skillBook.GetSkillPrice(_skillName));
            _skillBook.IncreaseSkillLevel(_skillName);
            GetSkillPrice();
            PriceChanged?.Invoke();
            RecountIfPlayerHasEnoughMoney();

            if (_skillName == "HealthSkill")
            {
                _player.RecountMaxHealth();
            }
        }
    }

    public void RecountIfPlayerHasEnoughMoney()
    {
        if (_player.Money >= _skillBook.GetSkillPrice(_skillName) && _skillBook.GetSkillLevel(_skillName) < _skillBook.GetSkillMaxLevel(_skillName))
        {
            _image.color = _enoughMoneyColor;
        }
        else
        {
            _image.color = _notEnoughMoneyColor;
        }
    }
}