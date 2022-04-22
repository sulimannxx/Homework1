using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        _player.HealthIsChanged += UpdateHpBar;
        _healthText.text = _player.CurrentHealth.ToString();
    }

    private void UpdateHpBar()
    {
        _slider.value = _player.CurrentHealth / _player.MaxHealth;
        _healthText.text = _player.CurrentHealth.ToString();
    }

    private void OnDisable()
    {
        _player.HealthIsChanged -= UpdateHpBar;
    }

    private void OnDestroy()
    {
        _player.HealthIsChanged -= UpdateHpBar;
    }
}
