using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Alarm : MonoBehaviour
{
    private bool _alarmIsWorking;
    private AudioSource _source;
    private SpriteRenderer _spriteRenderer;
    private bool _alarmVolumeIncreasing;

    private void Start()
    {
        _source = this.gameObject.GetComponent<AudioSource>();
        _source.volume = 0;
        _alarmVolumeIncreasing = true;
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_alarmVolumeIncreasing == true)
        {
            _source.volume += Time.deltaTime;

            if (_source.volume == 1)
            {
                _alarmVolumeIncreasing = false;
            }
        }

        if (_alarmVolumeIncreasing == false)
        {
            _source.volume -= Time.deltaTime;

            if (_source.volume == 0)
            {
                _alarmVolumeIncreasing = true;
            }
        }
    }

    public void SetAlarm(bool alarmState)
    {
        _alarmIsWorking = alarmState;
    }

    public void PlayAlarmSound()
    {
        _source.Play();
    }

    public void StopAlarmSound()
    {
        _source.Stop();
    }

    public void ChangeColor()
    {
        if (_alarmIsWorking)
        {
            _spriteRenderer.DOColor(Color.red, 1).SetLoops(3, LoopType.Yoyo);
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }
}
