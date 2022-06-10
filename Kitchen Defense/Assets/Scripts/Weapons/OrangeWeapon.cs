using UnityEngine;

public class OrangeWeapon : Weapon
{
    [SerializeField] private OrangeBullet _bullet;
    
    private Player _player;
    private SkillBook _skillBook;
    private string _orangeWeaponSkill = "OrangeWeaponSkill";
    private string _baseDamageSkill = "BaseDamageSkill";

    public float Damage { get; protected set; }
    protected float BulletSpeed = 5;

    private void Awake()
    {
        _player = Camera.main.GetComponent<ObjectFinder>().GetPlayer();
        _skillBook = _player.GetComponent<SkillBook>();
        _skillBook.SkillLevelChanged += OnSkillLevelChanged;
        BaseDamage = _player.SpellBook.GetSkillLevel(_baseDamageSkill) * _player.GloveDamageModifier;
        Damage = BaseDamage * _skillBook.GetSkillLevel(_orangeWeaponSkill);
        IsBought = true;
    }

    public override void Init(Player player)
    {
        Awake();
    }

    public override void OnSkillLevelChanged()
    {
        BaseDamage = _player.SpellBook.GetSkillLevel(_baseDamageSkill) * _player.GloveDamageModifier;
        Damage = BaseDamage * _skillBook.GetSkillLevel(_orangeWeaponSkill);
    }

    public override void Shoot(Player player)
    {
        Instantiate(_bullet, player.transform.position, Quaternion.identity);
    }

    public override bool Bought(bool state)
    {
        return IsBought = true;
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
