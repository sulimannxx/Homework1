using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _player.transform.Translate(-_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _player.transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
    }
}
