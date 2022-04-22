using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    protected float BaseHealth = 5;
    protected float BaseSpeed = 1;
    protected float BaseDamage = 1;
    protected int BaseReward = 1;
    protected Player _target;

    public abstract event UnityAction<Enemy> EnemyIsDead;

    public abstract void ApplyDamage(float damage);
    public abstract Player GetTarget();
    public abstract void Init(Player player);
    public abstract int GetReward();
}
