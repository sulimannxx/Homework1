using UnityEngine;

public class GranateWeapon : Weapon
{
    [SerializeField] private GranateWeapon _bullet;

    private Player _player;
    private string _baseDamageSkill = "BaseDamageSkill";
    private string _coolDownSkill = "CoolDownSkill";
    private SkillBook _skillBook;
    private string _granateWeaponSkill = "GranateWeaponSkill";

    protected float Damage;

    private void Awake()
    {
        _player = Camera.main.GetComponent<ObjectFinder>().GetPlayer();
        _skillBook = _player.GetComponent<SkillBook>();
        _skillBook.SkillLevelChanged += OnSkillLevelChanged;
        BaseDamage = 1 + _player.SpellBook.GetSkillLevel(_baseDamageSkill) * _player.GloveDamageModifier;
        Damage = BaseDamage * _skillBook.GetSkillLevel(_granateWeaponSkill) * 0.75f;
    }
    public override void Init(Player player)
    {
        Awake();
        _player = player;
        CoolDownCurrentValue = 0;
    }

    public override void OnSkillLevelChanged()
    {
        BaseDamage = 1 + _player.SpellBook.GetSkillLevel(_baseDamageSkill) * _player.GloveDamageModifier;
        Damage = BaseDamage * _skillBook.GetSkillLevel(_granateWeaponSkill) * 0.75f;
    }

    public override void Shoot(Player player)
    {
        Instantiate(_bullet, player.transform.position, Quaternion.identity);
        CoolDownBaseValue = (5 - player.SpellBook.GetSkillLevel(_coolDownSkill) / 200) - player.WeaponCoolDownModifier;
    }

    public override bool Bought(bool state)
    {
        return IsBought = state;
    }

    public override bool IsDamageCritical()
    {
        return _player.CriticalStrikeChance();
    }

    public override float GetDamage()
    {
        return Damage;
    }

    private void OnDestroy()
    {
        _skillBook.SkillLevelChanged -= OnSkillLevelChanged;
    }
}
