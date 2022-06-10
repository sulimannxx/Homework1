using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private GameObject _shopElement;
    [SerializeField] private GameObject[] _firstLayerShops;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private AudioSource _audioSource;

    private int _currentLayer = 0;

    private void ActivateZeroLayer()
    {
        foreach (var element in _firstLayerShops)
        {
            element.SetActive(false);
        }

        _mainMenu.SetActive(true);
        _currentLayer--;
    }

    public void OnButtonClick()
    {
        _audioSource.Play();

        switch (_currentLayer)
        {
            case 0:
                _shop.ActivateShopButtonPressedEvent(false);
                break;

            case 1:
                ActivateZeroLayer();
                break;
        }
    }

    public void IncreaseShopLayer()
    {
        _currentLayer++;
    }
}
