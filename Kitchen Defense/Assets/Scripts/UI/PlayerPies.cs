using TMPro;
using UnityEngine;

public class PlayerPies : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _player.MoneyChanged += OnMoneyAdded;
        _text.text = _player.PieCoins.ToString();
    }

    private void OnMoneyAdded()
    {
        _text.text = _player.PieCoins.ToString();
    }

    private void OnDestroy()
    {
        _player.MoneyChanged -= OnMoneyAdded;
    }
}
