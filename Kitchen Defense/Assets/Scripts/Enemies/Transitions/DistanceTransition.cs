using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    private float _transitionRange;
    private float _minValueDelta = 0;
    private float _maxValueDelta = 0.5f;

    private void Start()
    {
        _transitionRange += Random.Range(_minValueDelta, _maxValueDelta);
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
            {
                NeedToTransit = true;
            }
        }        
        else
        {
            NeedToTransit = false;
        }
    }
}
