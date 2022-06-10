using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ReceivePieText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Player _player;

    private float _value = 1;
    private Animator _animator;
    private string _animation = "PlayerHealTextAnimation";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player.PieCoinReceived += OnPieCoinReceived;
        gameObject.SetActive(false);
    }

    private void Start()
    {
    }

    private void OnPieCoinReceived()
    {
        gameObject.SetActive(true);
        _text.text = ($"+{_value}");
        _animator.Play(_animation);
        StartCoroutine(SwitchOffDelay());
    }

    private IEnumerator SwitchOffDelay()
    {
        yield return new WaitForSeconds(2);
        _animator.StopPlayback();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _player.PieCoinReceived -= OnPieCoinReceived;
    }
}