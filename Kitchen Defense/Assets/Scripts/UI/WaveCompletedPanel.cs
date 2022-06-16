using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class WaveCompletedPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _earnedSugarText;
    [SerializeField] private TMP_Text _earnedPiesText;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _adTimer;
    [SerializeField] private GameObject _adButton;
    [SerializeField] private GameObject _adText;

    private Image _winImage;

    private void Awake()
    {
        _winImage = GetComponent<Image>();
        _winImage.color = new Color(1, 1, 1, 0);
    }

    private void OnEnable()
    {
        _earnedSugarText.text = _player.EarnedMoneyOnThisWave.ToString();
        _earnedPiesText.text = _player.EarnedPiesOnThisWave.ToString();
        _adTimer.SetActive(true);
        _adButton.SetActive(true);
        _adText.SetActive(true);
    }
}

