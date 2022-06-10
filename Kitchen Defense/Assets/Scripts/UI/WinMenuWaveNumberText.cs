using TMPro;
using UnityEngine;

public class WinMenuWaveNumberText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _text.text = WaveController.GameWave.ToString();
    }
}
