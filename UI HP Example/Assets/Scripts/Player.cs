using UnityEngine;

public class Player : MonoBehaviour
{
    public float Health { get; private set; }

    private float _maxHP = 100;
    private float _minHP = 0;
    private float _startHP = 50;

    private void Start()
    {
        Health = _startHP;
    }

    public void SetHealth(float health)
    {
        Health += health;

        if (Health > _maxHP)
        {
            Health = 100;
        }

        if (Health < _minHP)
        {
            Health = 0;
        }
    }
}
