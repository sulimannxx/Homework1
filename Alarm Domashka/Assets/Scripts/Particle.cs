using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Particle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _spark;

    private AudioSource _source;


    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _spark.Play();
            _source.Play();
        }
    }
}
