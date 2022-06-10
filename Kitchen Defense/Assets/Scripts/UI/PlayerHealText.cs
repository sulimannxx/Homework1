using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerHealText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Player _player;
    [SerializeField] private IceCreamDispenser _iceCreamDispenser;

    private float _value;
    private Animator _animator;
    private string _animation = "PlayerHealTextAnimation";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _value = _iceCreamDispenser.HealPower;
        _text.text = ($"+{_value:f2}");
        _animator.Play(_animation);
        StartCoroutine(SwitchOffDelay());
    }

    private IEnumerator SwitchOffDelay()
    {
        yield return new WaitForSeconds(2);
        _animator.StopPlayback();
        this.gameObject.SetActive(false);
    }
}
