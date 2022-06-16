using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<BuyGloveButton> _buttons;

    private void OnEnable()
    {
        _player.GloveBuyButtonPressed += OnBuyButtonPressed;
    }

    private void OnBuyButtonPressed()
    {
        foreach (var button in _buttons)
        {
            button.RecountIfPlayerHasEnoughMoney();
        }
    }

    private void OnDisable()
    {
        _player.GloveBuyButtonPressed -= OnBuyButtonPressed;
    }
}
