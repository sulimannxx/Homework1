using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

    private const string IsWalking = "IsWalking";

    private void Start()
    {
        _animator.SetBool(IsWalking, true);
    }

    private void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x * _speed, 0);
    }
}
