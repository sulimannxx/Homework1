using UnityEngine;

[RequireComponent(typeof(DieState))]

public class EnemyDieTransition : Transition
{
    private Enemy _enemy;
    private DieState _dieState;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.EnemyIsDead += OnEnemyDead;
        _dieState = GetComponent<DieState>();
    }

    private void Start()
    {
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
