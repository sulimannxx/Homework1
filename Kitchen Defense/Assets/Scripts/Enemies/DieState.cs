using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(DeathDelay());
        _animator.Play(_animationDieState);
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopCoroutine(DeathDelay());
    }

    private void OnDisable()
    {
        StopCoroutine(DeathDelay());
    }
}
