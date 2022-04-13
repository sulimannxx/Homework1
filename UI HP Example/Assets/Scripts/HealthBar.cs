using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Color _lowHpColor;
    [SerializeField] private Color _highHpColor;
    [SerializeField] private Image _image;

    private Slider _slider;
    private float _sliderSpeed = 7f;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _player.Health, _sliderSpeed * Time.deltaTime);
    }

    public void ChangeSliderColor()
    {
        _image.color = Color.Lerp(_lowHpColor, _highHpColor, _slider.value / 100);
    }
}
