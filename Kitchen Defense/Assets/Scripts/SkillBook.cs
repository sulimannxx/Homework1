using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillBook : MonoBehaviour
{
    private ProgressSaveManager _saveManager;
    private Dictionary<string, Skill> _skillBook = new Dictionary<string, Skill>();

    public UnityAction SkillLevelChanged;

    private void Start()
    {
        _saveManager = Camera.main.GetComponent<ProgressSaveManager>();
        _skillBook.Add("WarehouseCapacitySkill", new Skill(1, 2, 15));
        _skillBook.Add("CriticalStrikeSkill", new Skill(1, 3, 25));
        _skillBook.Add("BaseDamageSkill", new Skill(1, 1, 50));
        _skillBook.Add("CoolDownSkill", new Skill(1, 2, 50));
        _skillBook.Add("HealthSkill", new Skill(1, 1, 50));
        _skillBook.Add("PieChanceSkill", new Skill(1, 5, 50));
        _skillBook.Add("OrangeWeaponSkill", new Skill(1, 1, 50));
        _skillBook.Add("StoneWeaponSkill", new Skill(1, 1, 50));
        _skillBook.Add("GranateWeaponSkill", new Skill(1, 1, 50));
        _skillBook.Add("LollipopWeaponSkill", new Skill(1, 1, 50));
        _skillBook.Add("SlushWeaponSkill", new Skill(1, 1, 50));
        _skillBook.Add("BananaWeaponSkill", new Skill(1, 1, 50));
        _skillBook.Add("EclairWeaponSkill", new Skill(1, 1, 50));
        LoadAllSkillLevels();
    }

    private void LoadAllSkillLevels()
    {
        int i = 0;

        foreach (var skill in _skillBook)
        {
            if (_saveManager.PlayerProfile.PlayerSkillLevels.Count > 0)
            {
                skill.Value.SetSkillLevel(_saveManager.PlayerProfile.PlayerSkillLevels[i]);
            }
            i++;

            if (skill.Value.Level == 0)
            {
                skill.Value.SetSkillLevel(1);
            }
        }
    }

    public void SaveSkillLevels()
    {
        _saveManager.PlayerProfile.PlayerSkillLevels.Clear();

        foreach (var skill in _skillBook)
        {
            _saveManager.SaveSkills(skill.Value.Level);
        }
    }

    public int GetSkillLevel(string key)
    {
        _skillBook.TryGetValue(key, out Skill skill);
        return skill.Level;
    }

    public int GetSkillMaxLevel(string key)
    {
        _skillBook.TryGetValue(key, out Skill skill);
        return skill.MaxLevel;
    }

    public void IncreaseSkillLevel(string key)
    {
        _skillBook.TryGetValue(key, out Skill skill);
        skill.IncreaseSkillLevel();
        SkillLevelChanged?.Invoke();
    }

    public float GetSkillPrice(string key)
    {
        _skillBook.TryGetValue(key, out Skill skill);
        return skill.Price;
    }

    public void RecountSkillPrice(string key)
    {
        _skillBook.TryGetValue(key, out Skill skill);
        skill.RecountSkillPrice();
    }
}

public class Skill
{
    private float _initialPrice;

    public int Level { get; private set; }
    public float Price { get; private set; }
    public int MaxLevel { get; private set; }

    public Skill(int level, float price, int maxLevel)
    {
        _initialPrice = price;
        Level = level;
        Price = price;
        MaxLevel = maxLevel;
    }

    public void IncreaseSkillLevel()
    {
        if (Level + 1 <= MaxLevel)
        {
            Level++;
        }
    }

    public void RecountSkillPrice()
    {
        Price = Mathf.Pow(Level, 4) * 10 * _initialPrice;
    }

    public void SetSkillLevel(int level)
    {
        Level = level;
        RecountSkillPrice();
    }
}

