using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PoisonDamage : LollipopWeapon
{
    private DamageTakenText _damageTakenEffect;
    private Transform _canvas;
    private DamageTakenText _damageTakenText;
    private Enemy _enemy;
    private float _poisonCooldown = 1;
    private Color _poisonColor = new Color(0,1,0,1);
    private AudioSource _audioSource;
    private Toggle _toggle;

    private void Start()
    {
        _toggle = Camera.main.GetComponent<ObjectFinder>().GeToggle();
        _enemy = GetComponent<Enemy>();
        StartCoroutine(EnemyReceivePoisonDamage());
        _enemy.GetComponent<SpriteRenderer>().material.color = _poisonColor;
        _canvas = Camera.main.GetComponent<ObjectFinder>().GetCanvas();
    }

    private IEnumerator EnemyReceivePoisonDamage()
    {
        while (_enemy.CurrentHealth > 0)
        {
            yield return new WaitForSeconds(_poisonCooldown);

            if (_toggle.isOn)
            {
                _audioSource.Play();
            }

            _damageTakenText = Instantiate(_damageTakenEffect, gameObject.transform.position,
                Quaternion.identity, _canvas);
            Destroy(_damageTakenText.gameObject, 0.5f);

            if (IsDamageCritical() == true)
            {
                _enemy.ApplyDamage(Damage * 2);
                _damageTakenText.SetTextValue(Damage * 2);
            }
            else
            {
                _enemy.ApplyDamage(Damage);
                _damageTakenText.SetTextValue(Damage);
            }
        }
    }

    public void InitPoisonDamage(DamageTakenText effect, AudioClip audioClip)
    {
        _damageTakenEffect = effect;
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.clip = audioClip;
    }
}
