using System.Collections;
using UnityEngine;

public class LollipopBullet : LollipopWeapon
{
    [SerializeField] private GameObject _lollipopSplash;
    [SerializeField] private DamageTakenText _damageTakenEffect;
    [SerializeField] private AudioClip _poisonDamageSound;

    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private ParticleSystem _particleSystem;
    private int _rotationSpeed = 500;

    private void Start()
    {
        StartCoroutine(BulletLifeTime());
        _particleSystem = _lollipopSplash.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, -1) * _rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _particleSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            _particleSystem.Play();
            Destroy(_particleSystem.gameObject, 2f);

            if (collision.gameObject.GetComponent<PoisonDamage>() == null)
            {
                enemy.gameObject.AddComponent<PoisonDamage>().InitPoisonDamage(_damageTakenEffect, _poisonDamageSound);
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator BulletLifeTime()
    {
        Destroy(gameObject, 5f);
        yield return null;
    }
}