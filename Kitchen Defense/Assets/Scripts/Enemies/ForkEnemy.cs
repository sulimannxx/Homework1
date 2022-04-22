using UnityEngine;
using UnityEngine.Events;

public class ForkEnemy : Enemy
{
    [SerializeField] private Player _player;

    private float _speed;
    private float _speedModifyer = 3;
    private float _damage;
    private float _health;

    protected float Speed;
    protected float Damage;

    public override event UnityAction<Enemy> EnemyIsDead;
    public int EnemyReward { get; private set; }

    private void Awake()
    {
        _health = BaseHealth;
        _damage = BaseDamage;
        _speed = BaseSpeed * _speedModifyer;
        Speed = _speed;
        Damage = _damage;
    }

    public override void Init(Player player)
    {
        _player = player;
        _player.PlayerIsDead += OnPlayerDead;
    }

    public override void ApplyDamage(float damage)
    {
        _health -= damage; 

        if (_health <= 0)
        {
            EnemyIsDead?.Invoke(this);
           Destroy(gameObject);
        }
    }

    public override Player GetTarget()
    {
        return _player;
    }

    public override int GetReward()
    {
        return EnemyReward += BaseReward * GameController.GameWave;
    }

    public void OnPlayerDead()
    {
        
    }

}
