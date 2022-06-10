using UnityEngine;

public class SoundInitialiser : MonoBehaviour
{
    [SerializeField] private MusicOptionsSwitch[] _switches;

    void Start()
    {
        foreach (var button in _switches)
        {
            button.Init();
        }
    }
}
