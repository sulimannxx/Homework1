using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _healPower = 10f;
    private float _hurtPower = -10f;

    public void ChangeHpValue()
    {
        if (TryGetComponent(out HealButton healButton))
        {
            _player.SetHealth(_healPower);
        }

        if (TryGetComponent(out HurtButton hurtButton))
        {
            _player.SetHealth(_hurtPower);
        }
    }
}
