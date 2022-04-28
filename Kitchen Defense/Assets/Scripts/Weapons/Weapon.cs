using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float BaseDamage = 1;

    public bool IsBought { get; protected set; }

    public abstract void Shoot();
    public abstract bool Bought(bool state);
}
