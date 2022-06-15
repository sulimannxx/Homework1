using TMPro;
using UnityEngine;

public class TextLanguageChanger : MonoBehaviour
{
    private TMP_Text _text;
    [TextArea]
    [SerializeField] private string _englishText;
    [TextArea]
    [SerializeField] private string _ukrainianText;
    [TextArea]
    [SerializeField] private string _russianText;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
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
            _text.text = _englishText;
        }
        else if (language == "ua")
        {
            _text.text = _ukrainianText;
        }
        else if (language == "ru")
        {
            _text.text = _russianText;
        }
    }
}
