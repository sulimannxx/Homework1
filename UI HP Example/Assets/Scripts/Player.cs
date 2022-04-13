using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _hurtButton;
    [SerializeField] private HealthBar _healthBar;

    public float Health { get; private set; }

    private float _maxHP = 100;
    private float _minHP = 0;
    private float _startHP = 50;
    private UnityAction _healAction;
    private UnityAction _hurtAction;
    private float _healPower = 10f;
    private float _hurtPower = -10f;

    private void Start()
    {
        Health = _startHP;
        _healAction += AddHealth;
        _hurtAction += SubtractHealth;
        _healButton.onClick.AddListener(_healAction);
        _hurtButton.onClick.AddListener(_hurtAction);
    }

    private void AddHealth()
    {
        Health += _healPower;

        if (Health > _maxHP)
        {
            Health = _maxHP;
        }
        _healthBar.ChangeHealthBarValue();
    }

    private void SubtractHealth()
    {
        Health += _hurtPower;

        if (Health < _minHP)
        {
            Health = _minHP;
        }
        _healthBar.ChangeHealthBarValue();
    }

}
