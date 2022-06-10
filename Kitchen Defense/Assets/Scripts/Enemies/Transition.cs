using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Player Target { get; private set; }
    protected Enemy ThisEnemy { get; private set; }

    public State TargetState => _targetState;
    public bool NeedToTransit { get; protected set; }

    public void Init(Player target, Enemy enemy)
    {
        Target = target;
        ThisEnemy = enemy;
    }

    private void OnEnable()
    {
        NeedToTransit = false;
    }
}
