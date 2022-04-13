using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Color _lowHpColor;
    [SerializeField] private Color _highHpColor;
    [SerializeField] private Image _image;

    private Slider _slider;
    private float _sliderSpeed = 7f;
    private float _sliderDefaultValue = 50.0f;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _sliderDefaultValue;
    }

    public void ChangeHealthBarValue()
    {
        StartCoroutine(ChangeSliderValue());
    }

    private IEnumerator ChangeSliderValue()
    {
        while (_slider.value != _player.Health)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _player.Health, _sliderSpeed * Time.deltaTime);
            yield return null;
        }
        yield break; 
    }

    public void ChangeSliderColor()
    {
        _image.color = Color.Lerp(_lowHpColor, _highHpColor, _slider.value / 100);
    }
}
