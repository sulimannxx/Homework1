using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float BaseDamage = 1;

    public abstract void Shoot();
}
