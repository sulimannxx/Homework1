using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float BaseDamage = 1;
    protected bool IsAlreadyBought;

    public bool IsBought => IsAlreadyBought;

    public abstract void Shoot();
    public abstract bool SetAsBought(bool state);
}
