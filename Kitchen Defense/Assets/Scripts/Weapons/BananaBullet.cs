using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class BananaBullet : BananaWeapon
{
    [SerializeField] private GameObject _bananaSplash;
    [SerializeField] private DamageTakenText _damageTakenEffect;
    [SerializeField] private AudioSource _audioSource;

    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private ParticleSystem _particleSystem;
    private Animator _animator;
    private string _bananaTraectoryAnimationName = "BananaTraectory";

    public float BulletDamage => Damage;

    private void Start()
    {
        _audioSource.Play();
        _animator = GetComponent<Animator>();
        Destroy(gameObject, 2.1f);
        _animator.Play(_bananaTraectoryAnimationName);
        _particleSystem = _bananaSplash.GetComponent<ParticleSystem>();
        _canvas = Camera.main.GetComponent<ObjectFinder>().GetCanvas();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _particleSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            _damageTakenText = Instantiate(_damageTakenEffect, collision.gameObject.transform.position,
                Quaternion.identity, _canvas);
            Destroy(_damageTakenText.gameObject, 0.5f);
            _particleSystem.Play();
            Destroy(_particleSystem.gameObject, 2f);

            if (IsDamageCritical() == true)
            {
                enemy.ApplyDamage(BulletDamage * 2);
                _damageTakenText.SetTextValue(BulletDamage * 2);
            }
            else
            {
                enemy.ApplyDamage(BulletDamage);
                _damageTakenText.SetTextValue(BulletDamage);
            }
        }
    }
}