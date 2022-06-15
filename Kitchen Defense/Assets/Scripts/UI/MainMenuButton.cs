using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private string _mainMenuSceneName = "MainMenu";

    public void OnButtonClick()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
        Time.timeScale = 1;
    }
}
