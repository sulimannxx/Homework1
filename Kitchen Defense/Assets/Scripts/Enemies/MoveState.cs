using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MoveState : State
{
    [SerializeField] private float _targetX;
    [SerializeField] private float _targetY;

    private float _speed;
    private string _animationMoveState = "Move";
    private Animator _animator;
    private Vector2 _targetPosition;
    private SlushFreezer _freezer;
    private float _defaultSpeed;
    private float _randomSpeedMinValue = 2.5f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _speed = Random.Range(_randomSpeedMinValue, _randomSpeedMinValue + WaveController.GameWave / 200f);
        _defaultSpeed = _speed;
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

    public void FreezeEnemy(float freezeTime, GameObject freezeTexture)
    {
        StartCoroutine(FreezeTimer(freezeTime, freezeTexture));
    }

    private IEnumerator FreezeTimer(float freezeTime, GameObject freezeTexture)
    {
        _speed = 0;
        yield return new WaitForSeconds(freezeTime);
        _speed = _defaultSpeed;
        Destroy(freezeTexture);
    }
}
