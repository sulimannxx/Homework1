using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MoveState))]
[RequireComponent(typeof(AttackState))]

public class DieState : State
{
    private Animator _animator;
    private string _animationDieState = "Die";
    private MoveState _moveState;
    private AttackState _attackState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveState = GetComponent<MoveState>();
        _attackState = GetComponent<AttackState>();
    }

    private void OnEnable()
    {
        _moveState.enabled = false;
        _attackState.enabled = false;
        Destroy(gameObject, 2f);
        _animator.Play(_animationDieState);
    }
}
