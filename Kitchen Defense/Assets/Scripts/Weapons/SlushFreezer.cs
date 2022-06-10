using UnityEngine;

public class SlushFreezer : SlushWeapon
{
    [SerializeField] private GameObject _freezeTexture;
    [SerializeField] private DamageTakenText _damageTakenEffect;

    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private GameObject _freezeEffect;
    private MoveState _moveState;

    private void Start()
    {
        Destroy(gameObject, 0.3f);
        _canvas = Camera.main.GetComponent<ObjectFinder>().GetCanvas();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _damageTakenText = Instantiate(_damageTakenEffect, collision.gameObject.transform.position,
                Quaternion.identity, _canvas);
            Destroy(_damageTakenText.gameObject, 0.5f);

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

            _moveState = enemy.GetComponent<MoveState>();
            _freezeEffect = Instantiate(_freezeTexture, enemy.transform.position, Quaternion.identity);
            Destroy(_freezeEffect, FreezeTime);
            _moveState.FreezeEnemy(FreezeTime, _freezeEffect);
        }
    }
}
