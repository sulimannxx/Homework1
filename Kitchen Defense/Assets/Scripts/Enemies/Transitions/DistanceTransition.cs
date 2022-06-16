using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _targetX;
    [SerializeField] private float _targetY;

    private float _transitionRange;
    private readonly float _minValueDelta = 0;
    private readonly float _maxValueDelta = 0.5f;
    private Vector2 _targetPosition;

    private void Start()
    {
        _targetPosition = new Vector2(Target.transform.position.x - _targetX, Target.transform.position.y - _targetY);
        _transitionRange += Random.Range(_minValueDelta, _maxValueDelta);
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector2.Distance(transform.position, _targetPosition) < _transitionRange) NeedToTransit = true;
        }
        else
        {
            NeedToTransit = false;
        }
    }
}