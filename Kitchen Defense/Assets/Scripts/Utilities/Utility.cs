using UnityEngine;

public abstract class Utility : MonoBehaviour
{
    public bool IsBought { get; protected set; }
    public abstract bool Bought(bool state);
    public abstract void Init();
}
