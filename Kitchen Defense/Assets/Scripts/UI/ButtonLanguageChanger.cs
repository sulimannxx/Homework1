using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguageChanger : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _enSprite;
    [SerializeField] private Sprite _uaSprite;
    [SerializeField] private Sprite _ruSprite;

    private void Start()
    {
        OnLanguageChanged(LanguageManager.CurrentLanguage);
        LanguageManager.LanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        LanguageManager.LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(string language)
    {
        if (language == "en")
        {
            _buttonImage.sprite = _enSprite;
        }
        else if (language == "ua")
        {
            _buttonImage.sprite = _uaSprite;
        }
        else if (language == "ru")
        {
            _buttonImage.sprite = _ruSprite;
        }
    }
}