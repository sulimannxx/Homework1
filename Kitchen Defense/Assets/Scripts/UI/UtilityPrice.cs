using TMPro;
using UnityEngine;

public class UtilityPrice : MonoBehaviour
{
    [SerializeField] private BuyUtilityButton _buyUtilityButton;
    [SerializeField] private TMP_Text _priceText;

    private void Start()
    {
        _buyUtilityButton.PriceChanged += OnPriceChanged;
    }

    private void OnEnable()
    {
        _priceText.text = _buyUtilityButton.UtilityPrice.ToString();
    }

    private void OnDestroy()
    {
        _buyUtilityButton.PriceChanged -= OnPriceChanged;
    }

    private void OnPriceChanged(int price)
    {
        _priceText.text = price.ToString();
    }
}