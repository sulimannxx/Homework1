using UnityEngine;

[RequireComponent(typeof(DieState))]
[RequireComponent(typeof(Enemy))]

public class EnemyDieTransition : Transition
{
    private DieState _dieState;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.IsDead += OnEnemyDead;
        _dieState = GetComponent<DieState>();
    }

    private void Update()
    {
        if (_enemy.GetHealth() <= 0)
        {
            _dieState.enabled = true;
            NeedToTransit = true;
        }
    }

    private void OnEnemyDead(Enemy enemy)
    {
        NeedToTransit = true;
    }
}