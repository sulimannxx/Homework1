using System.Collections;
using UnityEngine;

public class StoneBullet : StoneWeapon
{
    private int _rotationSpeed = 500;
    public float BulletDamage => Damage;

    private void Start()
    {
        StartCoroutine(BulletLifeTime());
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
            enemy.ApplyDamage(BulletDamage);
            Destroy(gameObject);
        }
    }

    private IEnumerator BulletLifeTime()
    {
        Destroy(gameObject, 5f);
        yield return null;
    }
}
