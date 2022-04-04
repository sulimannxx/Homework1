using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(SpriteRenderer))]

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _opened;
    [SerializeField] private Sprite _closed;
    [SerializeField] private Alarm _alarm;
    [SerializeField ]private SpriteRenderer _doorSprite;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _doorSprite.sprite = _opened;
            _alarm.SetAlarm(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _doorSprite.sprite = _closed;
            _alarm.SetAlarm(false);
        }
    }
}
