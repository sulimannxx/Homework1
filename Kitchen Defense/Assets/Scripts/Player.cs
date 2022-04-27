using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList;

    private float _orangeDamageSkillLevel = 1;
    private float _stoneDamageSkillLevel = 1;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex;
    private Animator _animator;
    private string _playerAttackAnimation = "Attack";
    private float _maxHealth = 5;
    private float _currentHealth;

    public float OrangeDamageSkillLevel => _orangeDamageSkillLevel;
    public float StoneDamageSkillLevel => _stoneDamageSkillLevel;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public event UnityAction PlayerIsDead;
    public event UnityAction HealthIsChanged;
    public event UnityAction MoneyChanged;
    public event UnityAction HasMoreThanOneWeapon;

    public int Money { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _currentWeapon = _weaponList[0];
        _currentWeaponIndex = 0;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.Play(_playerAttackAnimation);
            _currentWeapon.Shoot();
        }
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;
        HealthIsChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            PlayerIsDead?.Invoke();

            if (this.gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke();
    }

    public void AddWeaponToInventory(Weapon weapon, int price)
    {
        _weaponList.Add(weapon);
        Money -= price;
        MoneyChanged?.Invoke();
        HasMoreThanOneWeapon?.Invoke();
    }

    public void SwitchWeapon(int index)
    {

        _currentWeaponIndex += index;

        if (_currentWeaponIndex >= _weaponList.Count)
        {
            _currentWeaponIndex = 0;
            _currentWeapon = _weaponList[_currentWeaponIndex];
        }
        else if (_currentWeaponIndex < 0)
        {

            _currentWeaponIndex = _weaponList.Count - 1;
            _currentWeapon = _weaponList[_currentWeaponIndex];
        }
        else if (_currentWeaponIndex >= 0 && _currentWeaponIndex < _weaponList.Count)
        {
            _currentWeapon = _weaponList[_currentWeaponIndex];
        }
    }
}
