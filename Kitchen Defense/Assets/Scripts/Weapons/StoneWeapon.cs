using UnityEngine;

public class StoneWeapon : Weapon
{
    [SerializeField] private Player _player;
    [SerializeField] private StoneWeapon _bullet;

    protected float Damage;
    protected float BulletSpeed = 10;

    private void Awake()
    {       
        BaseDamage = 3;
        Damage = BaseDamage * _player.StoneDamageSkillLevel;
    }

    public override void Shoot()
    {
        Instantiate(_bullet, _player.transform.position, Quaternion.identity);
    }

    public override bool Bought(bool state)
    {
        return IsBought = state;
    }
}
