using UnityEngine;

public class SlushWeapon : Weapon
{ 
    [SerializeField] private SlushWeapon _bullet;

    private string _baseDamageSkill = "BaseDamageSkill";
    private string _coolDownSkill = "CoolDownSkill";
    private SkillBook _skillBook;
    private string _slushWeaponSkill = "SlushWeaponSkill";

    protected Player Player;
    protected float Damage;
    protected float FreezeTime;

    private void Awake()
    {
        Player = Camera.main.GetComponent<ObjectFinder>().GetPlayer();
        _skillBook = Player.GetComponent<SkillBook>();
        _skillBook.SkillLevelChanged += OnSkillLevelChanged;
        BaseDamage = 1 + Player.SpellBook.GetSkillLevel(_baseDamageSkill) * Player.GloveDamageModifier;
        Damage = BaseDamage * _skillBook.GetSkillLevel(_slushWeaponSkill) / 10;
        FreezeTime = 4 + _skillBook.GetSkillLevel(_slushWeaponSkill) / 25;
    }

    public override void Init(Player player)
    {
        Awake();
        Player = player;
        CoolDownCurrentValue = 0;
    }

    public override void OnSkillLevelChanged()
    {
        BaseDamage = 1 + Player.SpellBook.GetSkillLevel(_baseDamageSkill) * Player.GloveDamageModifier;
        Damage = BaseDamage * _skillBook.GetSkillLevel(_slushWeaponSkill) / 10f;
        FreezeTime = 4 + _skillBook.GetSkillLevel(_slushWeaponSkill) / 25f;
    }

    public override void Shoot(Player player)
    {
        Instantiate(_bullet, player.transform.position, Quaternion.identity);
        CoolDownBaseValue = (8 - player.SpellBook.GetSkillLevel(_coolDownSkill) / 200) - player.WeaponCoolDownModifier;
    }

    public override bool Bought(bool state)
    {
        return IsBought = state;
    }

    public override bool IsDamageCritical()
    {
        return Player.CriticalStrikeChance();
    }

    public override float GetDamage()
    {
        return FreezeTime;
    }

    private void OnDestroy()
    {
        _skillBook.SkillLevelChanged -= OnSkillLevelChanged;
    }
}