using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IceCreamDispenser : Utility
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _iceCreamTemplate;
    [SerializeField] private TMP_Text _text;

    private GameObject _iceCream;
    private Animator _animator;
    private string _dispenserAnimationName = "DispenserAnimation";
    private string _defaultDispenserAnimationName = "DefaultDispenserAnimation";
    private float _minRandomTimeValue = 8f;
    private float _maxRandomTimeValue = 12f;

    public float HealPower { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.IceCreamIsBought == true)
        {
            EnableDispenser();
            IsBought = true;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(IceCreamHeal());
        Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.IceCreamIsBought = true;
    }

    private IEnumerator IceCreamHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minRandomTimeValue, _maxRandomTimeValue));

            if (_player.isActiveAndEnabled)
            {
                _animator.Play(_defaultDispenserAnimationName);
                _animator.Play(_dispenserAnimationName);
                _iceCream = Instantiate(_iceCreamTemplate, transform.parent);
                HealPower = Random.Range(WaveController.GameWave / 10f, WaveController.GameWave / 5f);
                _player.Heal(HealPower);
                _text.gameObject.SetActive(true);
                Destroy(_iceCream, 1);
            }
        }
    }

    private void EnableDispenser()
    {
        this.gameObject.SetActive(true);
    }

    public override bool EnableUtility(bool state)
    {
        if (state == true)
        {
            EnableDispenser();
        }

        return IsBought = state;
    }

    public override void Init()
    {
        Start();
    }
}
