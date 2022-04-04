using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sphere))]

public class Spawner : MonoBehaviour
{
    private static readonly int _spawnersAmount = 3;

    [SerializeField] private Transform _sphere;
    [SerializeField] private Transform[] _spawnPoint = new Transform[_spawnersAmount];

    private string _spawnMethodName = "SpawnSphere";
    private float _spawnTime = 2;
    private int _counter = 0;

    private void Start()
    {
        InvokeRepeating(_spawnMethodName, _spawnTime, _spawnTime);
    }

    private void SpawnSphere()
    {
        if (_counter > 2)
        {
            _counter = 0;
        }

        Instantiate(_sphere, new Vector3(_spawnPoint[_counter].position.x, _spawnPoint[_counter].position.y, _spawnPoint[_counter].position.z), Quaternion.identity);
        IncrementCounter();
    }

    private void IncrementCounter()
    {
        ++_counter;
    }
}
