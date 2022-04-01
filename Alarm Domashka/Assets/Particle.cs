using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private AudioSource _source;


    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _particleSystem.Play();
            _source.Play();
        }
    }
}
