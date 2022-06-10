using UnityEngine;
using UnityEngine.UI;

public class ObjectLanguageChanger : MonoBehaviour
{
    [SerializeField] private Sprite _enSprite;
    [SerializeField] private Sprite _uaSprite;
    [SerializeField] private Sprite _ruSprite;
    [SerializeField] private Image _playButtonSprite;

    private void Start()
    {
        LanguageManager.LanguageChanged += OnLanguageChanged;

        if (PlayerPrefs.GetString("language") != null)
        {
            LanguageManager.CurrentLanguage = PlayerPrefs.GetString("language");
            OnLanguageChanged(PlayerPrefs.GetString("language"));
        }
    }

    private void OnDestroy()
    {
        LanguageManager.LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(string language)
    {
        if (language == "en")
        {
            _playButtonSprite.sprite = _enSprite;
        }
        else if (language == "ua")
        {
            _playButtonSprite.sprite = _uaSprite;
        }
        else if (language == "ru")
        {
            _playButtonSprite.sprite = _ruSprite;
        }
    }
}
