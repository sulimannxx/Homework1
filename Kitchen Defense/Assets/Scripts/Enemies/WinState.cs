using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class WinState : State
{
    private Animator _animator;
    private string _animationWinState = "Win";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(_animationWinState);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
