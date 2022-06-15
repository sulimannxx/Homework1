using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _maxHealth;
    [SerializeField] private TMP_Text _baseDamage;
    [SerializeField] private TMP_Text _critChance;
    [SerializeField] private TMP_Text _cooldown;
    [SerializeField] private TMP_Text _cakeChance;
    [SerializeField] private TMP_Text _wallHealth;
    [SerializeField] private TMP_Text _sugarFarmPerMinute;
    [SerializeField] private TMP_Text _icecreamHeal;
    [SerializeField] private TMP_Text _orangeWeaponDamage;
    [SerializeField] private TMP_Text _stoneWeaponDamage;
    [SerializeField] private TMP_Text _granateWeaponDamage;
    [SerializeField] private TMP_Text _lollipopPoisonDamagePerSecond;
    [SerializeField] private TMP_Text _slushFreezeTimeLvl;
    [SerializeField] private TMP_Text _bananaWeaponDamage;
    [SerializeField] private TMP_Text _eclairDamage;
    [SerializeField] private TMP_Text _sugarBonus;

    private string _baseDamageSkill = "BaseDamageSkill";
    private string _criticalStrikeSkill = "CriticalStrikeSkill";
    private string _pieChanceSkill = "PieChanceSkill";
    private string _coolDownSkill = "CoolDownSkill";
    private string _slushWeaponSkill = "SlushWeaponSkill";

    private void OnEnable()
    {
        Time.timeScale = 0;
        _maxHealth.text = Convert.ToInt32(_player.MaxHealth).ToString();
        _baseDamage.text = _player.SpellBook.GetSkillLevel(_baseDamageSkill).ToString();
        _critChance.text = $"{(_player.SpellBook.GetSkillLevel(_criticalStrikeSkill) + _player.CriticalStrikeModifier).ToString()}%";
        _cooldown.text = $"-{(_player.SpellBook.GetSkillLevel(_coolDownSkill) / 200f + _player.WeaponCoolDownModifier).ToString()} sec";
        _cakeChance.text = $"{(_player.SpellBook.GetSkillLevel(_pieChanceSkill) / 35f + 1f + _player.PieChanceModifier):0.00}%";
        _wallHealth.text = (4 + WaveController.GameWave).ToString();
        _sugarFarmPerMinute.text = (10 * WaveController.GameWave).ToString();
        _icecreamHeal.text = (WaveController.GameWave / 10f).ToString();
        _orangeWeaponDamage.text = GetWeaponDamage<OrangeWeapon>();
        _stoneWeaponDamage.text = GetWeaponDamage<StoneWeapon>();
        _granateWeaponDamage.text = GetWeaponDamage<GranateWeapon>();
        _lollipopPoisonDamagePerSecond.text = GetWeaponDamage<LollipopWeapon>();
        _slushFreezeTimeLvl.text = (4 + _player.SpellBook.GetSkillLevel(_slushWeaponSkill) / 25f).ToString();
        _bananaWeaponDamage.text = GetWeaponDamage<BananaWeapon>();
        _eclairDamage.text = GetWeaponDamage<EclairWeapon>();
        _sugarBonus.text = $"{Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.MoneyIncomeBonus:0.0}%";
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private string GetWeaponDamage<T>() where T : Weapon
    {
        Weapon weapon = _player.GetWeapon<T>();

        if (weapon != null)
        {
            weapon.Init(_player);
            return weapon.GetDamage().ToString();
        }

        return "0";
    }
}
