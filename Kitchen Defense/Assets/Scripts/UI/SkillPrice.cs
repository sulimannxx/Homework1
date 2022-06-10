using TMPro;
using UnityEngine;

public class SkillPrice : MonoBehaviour
{
    [SerializeField] private BuySkillButton _buySkillButton;
    [SerializeField] private TMP_Text _priceText;

    private void Start()
    {
        RecountTextPrice();
    }

    private void OnEnable()
    {
        _buySkillButton.PriceChanged += OnPriceChanged;
    }

    private void OnPriceChanged()
    {
        RecountTextPrice();
    }

    private void RecountTextPrice()
    {
        _priceText.text = _buySkillButton.SkillPrice.ToString();
    }

    private void OnDisable()
    {
        _buySkillButton.PriceChanged -= OnPriceChanged;
    }
}