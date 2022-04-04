using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public class Alarm : MonoBehaviour
{
    private AudioSource _source;
    private SpriteRenderer _spriteRenderer;
    private bool _alarmVolumeIncreasing;
    private bool _isWorking;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.volume = 0;
        _alarmVolumeIncreasing = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator AlarmSound()
    {
        while (_alarmVolumeIncreasing == true)
        {
            _source.volume += Time.deltaTime;
            yield return null;
        }

        while (_alarmVolumeIncreasing == false)
        {
            _source.volume -= Time.deltaTime;
            yield return null;
        }
    }

    public void SetAlarm(bool alarmState)
    {
        _isWorking = alarmState;

        if (_isWorking)
        {
            _alarmVolumeIncreasing = true;
            PlaySound();
            ChangeColor();
            StartCoroutine(AlarmSound());
        }
        else
        {
            ChangeColor();
            _alarmVolumeIncreasing = false;
        }
    }

    public void PlaySound()
    {
        _source.Play();
    }

    public void StopSound()
    {
        _source.Stop();
    }

    public void ChangeColor()
    {
        if (_isWorking)
        {
            _spriteRenderer.DOColor(Color.red, 1).SetLoops(3, LoopType.Yoyo);
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }
}
