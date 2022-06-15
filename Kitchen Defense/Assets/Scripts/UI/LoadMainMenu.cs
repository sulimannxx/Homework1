using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    private string _mainMenuSceneName = "MainMenu";

    private void Start()
    {
        StartCoroutine(LoadMainMenuScene());
    }

    private IEnumerator LoadMainMenuScene()
    {
        yield return new WaitForSeconds((float) _videoPlayer.clip.length + 1f);
        SceneManager.LoadScene(1);
    }
}
