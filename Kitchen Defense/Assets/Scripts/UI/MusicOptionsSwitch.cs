using UnityEngine;
using UnityEngine.UI;

public class MusicOptionsSwitch : MonoBehaviour
{
    [SerializeField] private AudioSource[] _soundSources;
    [SerializeField] private string _toggleSettingsName;
    [SerializeField] private bool _pressedFromMainGameScene;

    private Toggle _toggle;

    private int _onValue = 0;
    private int _offValue = 1;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        SetToggle(PlayerPrefs.GetInt(_toggleSettingsName));
    }

    private void SetToggle(int state)
    {
        if (state == _offValue)
        {
            _toggle.isOn = false;
            SoundSwitcher();
        }
        else if (state == _onValue)
        {
            _toggle.isOn = true;
            SoundSwitcher();
        }
    }


    public void SoundSwitcher()
    {
        if (_toggle.isOn == false)
        {
            PlayerPrefs.SetInt(_toggleSettingsName, _offValue);

            foreach (var sound in _soundSources)
            {
                sound.enabled = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt(_toggleSettingsName, _onValue);

            foreach (var sound in _soundSources)
            {
                sound.enabled = true;

                if (!sound.isPlaying && _toggleSettingsName != "sound")
                {
                    sound.Play();

                    if (_pressedFromMainGameScene)
                    {
                        sound.Pause();
                    }
                }
            }
        }
    }

    public void Init()
    {
        Start();
    }
}
