using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _player.MoneyChanged += OnMoneyAdded;
        _text.text = _player.Money.ToString();
    }

    private void OnMoneyAdded()
    {
        _text.text = _player.Money.ToString();
    }

    private void OnDestroy()
    {
        _player.MoneyChanged -= OnMoneyAdded;
    }
}
