using TMPro;
using UnityEngine;

public class DamageTakenText : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageTakenText;

    public void SetTextValue(float damage)
    {
        _damageTakenText.text = $"{damage:f2}";
    }
}
