using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MoveState : State
{
    [SerializeField] private float _speed;

    private string _animationMoveState = "Move";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Target == null)
        {
            GetNextState();
        }

        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _animator.Play(_animationMoveState);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
