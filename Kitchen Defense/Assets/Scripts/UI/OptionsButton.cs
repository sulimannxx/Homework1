using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuButtons;
    [SerializeField] private GameObject _optionsMenu;

    public void OnButtonClick()
    {
        _pauseMenuButtons.SetActive(false);
        _optionsMenu.SetActive(true);
    }
}
