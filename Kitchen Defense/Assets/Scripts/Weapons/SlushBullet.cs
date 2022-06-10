using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class SlushBullet : SlushWeapon
{
    [SerializeField] private GameObject _slushSplashEffect;
    [SerializeField] private GameObject _slushExplosion;

    private ParticleSystem _particleSystem;
    private Animator _animator;
    private string _slushTraectoryAnimationName = "SlushTraectory";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(BulletLifeTime());
        _animator.Play(_slushTraectoryAnimationName);
        _particleSystem = _slushSplashEffect.GetComponent<ParticleSystem>();
    }
    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(1);
        _particleSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
        _particleSystem.Play();
        Destroy(_particleSystem.gameObject, 2f);
        _slushExplosion = Instantiate(_slushExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}