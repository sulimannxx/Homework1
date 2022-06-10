using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AttackState : State
{
    [SerializeField] private float _delay;

    private float _damage;
    private float _lastAttackTime;
    private Animator _animator;
    private string _animationAttackState = "Fork Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damage = GetComponent<Enemy>().BaseDamage;
    }

    private void Update()
    {
        if (_lastAttackTime <= 0 && Target.gameObject.activeSelf == true)
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _animator.Play(_animationAttackState);
        target.ApplyDamage(_damage);
    }
}
