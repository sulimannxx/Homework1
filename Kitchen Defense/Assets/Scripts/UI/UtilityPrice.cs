using TMPro;
using UnityEngine;

public class UtilityPrice : MonoBehaviour
{
    [SerializeField] private BuyUtilityButton _buyUtilityButton;
    [SerializeField] private TMP_Text _priceText;

    private void OnEnable()
    {
        _priceText.text = _buyUtilityButton.UtilityPrice.ToString();
    }
}