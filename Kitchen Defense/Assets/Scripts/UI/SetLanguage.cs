using UnityEngine;

public class SetLanguage : MonoBehaviour
{
    [SerializeField] private AudioSource _tapAudioSource;

    public void SetUkrainianLanguage()
    {
        _tapAudioSource.Play();
        LanguageManager.SetCurrentLanguage("ua");
    }

    public void SetEnglishLanguage()
    {
        _tapAudioSource.Play();
        LanguageManager.SetCurrentLanguage("en");
    }

    public void SetRussianLanguage()
    {
        _tapAudioSource.Play();
        LanguageManager.SetCurrentLanguage("ru");
    }
}
