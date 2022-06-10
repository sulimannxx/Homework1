using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    protected float BaseHealth = 5;
    protected float BaseSpeed = 1;
    protected int BaseReward = 1;

    public float BaseDamage { get; protected set; }

    public abstract event UnityAction<Enemy> EnemyIsDead;
    public abstract event UnityAction<Enemy> EnemyIsHit;
    public float MaxHealth { get; protected set; }
    public float CurrentHealth { get; protected set; }
    public SpriteRenderer EnemySprite { get; protected set; }

    public abstract void ApplyDamage(float damage);
    public abstract Player GetTarget();
    public abstract void Init(Player player);
    public abstract int GetReward();
    public abstract float GetHealth();
}
