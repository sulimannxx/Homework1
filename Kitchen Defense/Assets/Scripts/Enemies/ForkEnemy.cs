using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class ForkEnemy : Enemy
{
    [SerializeField] private Player _player;

    private float _speed;
    private float _speedModifyer = 3;
    private float _damage;
    private float _health;
    private Animator _animator;
    private string _animationWinState = "Win";
    private BoxCollider2D _boxCollider;

    protected float Speed;
    protected float Damage;

    public override event UnityAction<Enemy> EnemyIsDead;
    public override event UnityAction<Enemy> EnemyIsHit;

    public int EnemyReward { get; private set; }

    private void Awake()
    {
        _health = BaseHealth;
        _damage = BaseDamage;
        _speed = BaseSpeed * _speedModifyer;
        Speed = _speed;
        Damage = _damage;
        MaxHealth = 5;
        _boxCollider = GetComponent<BoxCollider2D>();
        CurrentHealth = _health;
    }

    public override void Init(Player player)
    {
        _player = player;
        _player.PlayerIsDead += OnPlayerDead;
        EnemySprite = GetComponent<SpriteRenderer>();
    }

    public override void ApplyDamage(float damage)
    {
        _health -= damage;
        CurrentHealth = _health;
        EnemyIsHit?.Invoke(this);

        if (_health <= 0)
        {
            EnemyIsDead?.Invoke(this);
            _boxCollider.enabled = false;
        }
    }

    public override Player GetTarget()
    {
        return _player;
    }

    public override int GetReward()
    {
        return EnemyReward += BaseReward * WaveController.GameWave;
    }

    public override float GetHealth()
    {
        return _health;
    }

    public void OnPlayerDead()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(_animationWinState);
    }

    private void OnDestroy()
    {
        _player.PlayerIsDead -= OnPlayerDead;
    }
}
