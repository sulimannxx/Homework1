using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _openedDoor;
    [SerializeField] private Sprite _closedDoor;
    [SerializeField] private Alarm _alarm;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _openedDoor;
            _alarm.SetAlarm(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _closedDoor;
            _alarm.SetAlarm(false);
        }
    }
}
