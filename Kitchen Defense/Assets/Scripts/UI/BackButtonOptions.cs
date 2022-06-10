using UnityEngine;

public class BackButtonOptions : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenuButtons;
    [SerializeField] private GameObject _pauseMenuButtons;

    public void OnButtonClick()
    {
        _optionsMenuButtons.SetActive(false);
        _pauseMenuButtons.SetActive(true);
    }
}
