using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList;

    private float _orangeDamageSkillLevel = 1;
    private Weapon _currentWeapon;
    private Animator _animator;
    private string _playerAttackAnimation = "Attack";
    private float _health = 5;

    public float OrangeDamageSkillLevel => _orangeDamageSkillLevel;
    public event UnityAction PlayerIsDead;

    public int Money { get; private set; }

    private void Start()
    {
        _currentWeapon = _weaponList[0];
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
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void AddMoney(int reward)
    {
        Money += reward;
    }
}
