using System.Collections;
using UnityEngine;

public class MainMenuSettings : MonoBehaviour
{
    [SerializeField] private GameObject[] _settingsButtons;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MusicOptionsSwitch[] _musicOptionsSwitches;

    private bool _areSettingsOpen = false;
    private string _settingsOpenAnimation = "SettingsBackground";
    private string _settingsCloseAnimation = "SettingsBackgroundClose";

    private void Start()
    {
        foreach (var button in _musicOptionsSwitches)
        {
            button.Init();
        }
    }

    public void OnButtonClick()
    {
        _audioSource.Play();

        if (_areSettingsOpen == false)
        {
            StartCoroutine(SettingsButtonSwitch(1, true, _settingsOpenAnimation));
        }
        else
        {
            StartCoroutine(SettingsButtonSwitch(0, false, _settingsCloseAnimation));
        }
    }

    private IEnumerator SettingsButtonSwitch(float time, bool state, string animationName)
    {
        _animator.Play(animationName);
        _areSettingsOpen = state;

        yield return new WaitForSeconds(time);

        foreach (var button in _settingsButtons)
        {
            button.SetActive(state);
        }
    }
}
