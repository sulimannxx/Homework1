using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MoveState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _targetX;
    [SerializeField] private float _targetY;

    private string _animationMoveState = "Move";
    private Animator _animator;
    private Vector2 _targetPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _targetPosition = new Vector2(Target.transform.position.x - _targetX, Target.transform.position.y - _targetY);
    }

    private void Update()
    {
        if (Target == null)
        {
            GetNextState();
        }

        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
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
