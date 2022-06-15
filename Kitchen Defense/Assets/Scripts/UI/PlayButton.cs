using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private string _mainGameSceneName = "MainGame";

    public void OnButtonClick()
    {
        SceneManager.LoadScene(_mainGameSceneName);
    }
}
