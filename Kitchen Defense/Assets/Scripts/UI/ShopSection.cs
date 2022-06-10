using UnityEngine;

public class ShopSection : MonoBehaviour
{
    [SerializeField] private BackButton _backButton;
    [SerializeField] private GameObject _shopSection;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private AudioSource _audioSource;

    public void OnButtonClick()
    {
        _audioSource.Play();
        _backButton.IncreaseShopLayer();
        _shopSection.SetActive(true);
        _mainMenu.SetActive(false);
    }
}
