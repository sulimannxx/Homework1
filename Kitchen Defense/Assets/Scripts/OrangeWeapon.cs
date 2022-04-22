using UnityEngine;

public class OrangeWeapon : Weapon
{
    [SerializeField] private Player _player;
    [SerializeField] private OrangeBullet _bullet;

    protected float Damage;
    protected float BulletSpeed = 5;

    private void Awake()
    {
        Damage = BaseDamage * _player.OrangeDamageSkillLevel;
    }

    public override void Shoot()
    {
       Instantiate(_bullet, _player.transform.position, Quaternion.identity);
    }
}
