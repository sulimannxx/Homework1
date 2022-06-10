using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static int GameWave = 1;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _nextWaveButton;
    [SerializeField] private GameObject _repeatWaveButton;
    [SerializeField] private GameObject _previousWaveButton;
    [SerializeField] private Image _winImage;
    [SerializeField] private Image _gameOverImage;
    [SerializeField] private Image _nextWaveImage;
    [SerializeField] private Player _player;

    public float EarnedSugarOnThisWave { get; private set; }
    public float EarnedPiesOnThisWave { get; private set; }

    private void Start()
    {
        _player.PlayerIsDead += EnableGameOverScreen;
    }

    private void OnDestroy()
    {
        _player.PlayerIsDead -= EnableGameOverScreen;
    }

    private IEnumerator AddAlphaValueToObject(GameObject uiGameobject, Image uiImage)
    {
        uiGameobject.SetActive(true);

        while (uiImage.color.a < 1)
        {
            uiImage.color = new Color(1, 1, 1, uiImage.color.a + Time.unscaledDeltaTime * 5);
            yield return null;
        }
    }

    public void EnableWinScreen()
    {
        _winImage.color = new Color(1, 1, 1, 0);
        DisableGameOverButtons();
        StartCoroutine((AddAlphaValueToObject(_winScreen, _winImage)));
        _player.SetPlayerShootAbility(false);
        EarnedSugarOnThisWave = _player.EarnedMoneyOnThisWave;
        EarnedPiesOnThisWave = _player.EarnedPiesOnThisWave;
    }

    public void EnableNextWaveButton()
    {
        _nextWaveImage.color = new Color(1, 1, 1, 0);
        StartCoroutine((AddAlphaValueToObject(_nextWaveButton, _nextWaveImage)));
    }

    public void EnableGameOverScreen()
    {
        _gameOverImage.color = new Color(1, 1, 1, 0);
        StartCoroutine((AddAlphaValueToObject(_gameOverScreen, _gameOverImage)));
        _player.SetPlayerShootAbility(false);
        EarnedSugarOnThisWave = _player.EarnedMoneyOnThisWave;
        EarnedPiesOnThisWave = _player.EarnedPiesOnThisWave;
    }

    public void EnableGameOverButtons()
    {
        _previousWaveButton.SetActive(true);
        _repeatWaveButton.SetActive(true);
    }

    public void DisableGameOverButtons()
    {
        _previousWaveButton.SetActive(false);
        _repeatWaveButton.SetActive(false);
    }

    public void StartAddAlphaValueToObjectCoroutine(GameObject uiGameobject, Image uiImage)
    {
        StartCoroutine((AddAlphaValueToObject(uiGameobject, uiImage)));
    }
}
