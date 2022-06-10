using System.Collections;
using UnityEngine;

public class StoneBullet : StoneWeapon
{
    [SerializeField] private GameObject _stoneSplash;
    [SerializeField] private DamageTakenText _damageTakenEffect;

    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private ParticleSystem _particleSystem;
    private int _rotationSpeed = 500;

    private void Start()
    {
        StartCoroutine(BulletLifeTime());
        _particleSystem = _stoneSplash.GetComponent<ParticleSystem>();
        _canvas = Camera.main.GetComponent<ObjectFinder>().GetCanvas();
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
            _damageTakenText = Instantiate(_damageTakenEffect, collision.gameObject.transform.position,
                Quaternion.identity, _canvas);
            Destroy(_damageTakenText.gameObject, 0.5f); 
            _particleSystem.Play();
            Destroy(_particleSystem.gameObject, 2f);

            if (IsDamageCritical() == true)
            {
                enemy.ApplyDamage(Damage * 2);
                _damageTakenText.SetTextValue(Damage * 2);
            }
            else
            {
                enemy.ApplyDamage(Damage);
                _damageTakenText.SetTextValue(Damage);
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
