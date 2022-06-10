using System.Collections;
using UnityEngine;

public class OrangeBullet : OrangeWeapon
{
    [SerializeField] private GameObject _juiceSplash;
    [SerializeField] private DamageTakenText _damageTakenEffect;

    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private ParticleSystem _particleSystem;
    private int _rotationSpeed = 500;
    public float BulletDamage => Damage;

    private void Start()
    {
        StartCoroutine(BulletLifeTime());
        _particleSystem = _juiceSplash.GetComponent<ParticleSystem>();
        _canvas = Camera.main.GetComponent<ObjectFinder>().GetCanvas();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0,0,-1) * _rotationSpeed * Time.deltaTime, Space.Self);
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

            Destroy(gameObject);
        }
    }

    private IEnumerator BulletLifeTime()
    {
        Destroy(gameObject, 5f);
        yield return null;
    }
}
