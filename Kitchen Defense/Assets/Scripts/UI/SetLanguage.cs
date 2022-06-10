using UnityEngine;

public class SetLanguage : MonoBehaviour
{
    public void SetUkrainianLanguage()
    {
        LanguageManager.SetCurrentLanguage("ua");
    }

    public void SetEnglishLanguage()
    {
        LanguageManager.SetCurrentLanguage("en");
    }

    public void SetRussianLanguage()
    {
        LanguageManager.SetCurrentLanguage("ru");
    }
}
