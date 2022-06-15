using TMPro;
using UnityEngine;

public class BonusSugarValue : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _text.text = $"{(WaveController.GameWave / 10f).ToString()}%";
    }
}
