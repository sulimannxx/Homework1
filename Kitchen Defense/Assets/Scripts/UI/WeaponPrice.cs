using TMPro;
using UnityEngine;

public class WeaponPrice : MonoBehaviour
{
    [SerializeField] private BuyWeaponButton _buyWeaponButton;
    [SerializeField] private TMP_Text _priceText;

    private void Start()
    {
        _priceText.text = _buyWeaponButton.WeaponPrice.ToString();
    }
}
