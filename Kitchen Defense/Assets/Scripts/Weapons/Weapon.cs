using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float BaseDamage = 1;

    public bool IsBought { get; protected set; }
    public float CoolDownBaseValue { get; protected set; }
    public float CoolDownCurrentValue { get; protected set; } = 0;

    public void SetCurrentCoolDownValue(float value)
    {
        CoolDownCurrentValue = value;
    }

    public virtual void Init(Player player)
    {}

    public abstract void Shoot(Player player);
    public abstract bool Bought(bool state);
    public abstract bool IsDamageCritical();
    public abstract float GetDamage();
    public abstract void OnSkillLevelChanged();
}
