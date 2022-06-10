using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour
{
    public static string EnglishLanguage = "en";
    public static string UkrainianLanguage = "ua";
    public static string RussianLanguage = "ru";
    public static string DefaultLanguage = "en";
    public static string CurrentLanguage = DefaultLanguage;

    public static UnityAction<string> LanguageChanged;

    private void Awake()
    {
        GetCurrentLanguage();
    }

    public static void GetCurrentLanguage()
    {
        if (PlayerPrefs.GetString("language") != null)
        {
            CurrentLanguage = PlayerPrefs.GetString("language");
        }
        else
        {
            CurrentLanguage = DefaultLanguage;
        }
    }

    public static void SetCurrentLanguage(string language)
    {
        LanguageChanged?.Invoke(language);
        PlayerPrefs.SetString("language", language);
        CurrentLanguage = language;
    }
}
