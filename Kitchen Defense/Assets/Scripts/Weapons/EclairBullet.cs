using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EclairBullet : EclairWeapon
{
    [SerializeField] private GameObject _eclairExplosionEffect;
    [SerializeField] private Sprite _eclairTriggeredSprite;
    [SerializeField] private DamageTakenText _damageTakenEffect;
    [SerializeField] private AudioSource _audioSource;

    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private ParticleSystem _particleSystem;
    private Animator _animator;
    private string _eclairTraectoryAnimationName = "EclairTraectory";
    private float _delay;
    private SpriteRenderer _spriteRenderer;
    private bool _mineIsReady = false;

    public float BulletDamage => Damage;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _delay = 5;
        _animator = GetComponent<Animator>();
        _animator.Play(_eclairTraectoryAnimationName);
        StartCoroutine(TriggerDelay());
        _particleSystem = _eclairExplosionEffect.GetComponent<ParticleSystem>();
        _canvas = Camera.main.GetComponent<ObjectFinder>().GetCanvas();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (_mineIsReady)
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

                Destroy(gameObject);
            }
        }
    }

    private IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(_delay);
        _spriteRenderer.sprite = _eclairTriggeredSprite;
        _mineIsReady = true;
        _audioSource.Play();
    }
}