using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GranateBullet : GranateWeapon
{
    [SerializeField] private GameObject _pitExplosionEffect;
    [SerializeField] private GameObject _pitExplosion;

    private ParticleSystem _particleSystem;
    private Animator _animator;
    private string _granateTraectoryAnimationName = "GranateTraectory";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(BulletLifeTime());
        _animator.Play(_granateTraectoryAnimationName);
        _particleSystem = _pitExplosionEffect.GetComponent<ParticleSystem>();
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(2);
        _particleSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
        _particleSystem.Play();
        Destroy(_particleSystem.gameObject, 2f);
        _pitExplosion = Instantiate(_pitExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
