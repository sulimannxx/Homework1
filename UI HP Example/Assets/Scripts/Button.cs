using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private float _targetValue;
    private float _healPower = 10f;
    private float _hurtPower = -10f;
    private bool _healButtonPressed = false;
    private bool _hurtButtonPressed = false;
    private float _sliderSpeed = 7f;
    private float _maxHP = 100;
    private float _minHP = 0;

    private void Update()
    {
        if (_healButtonPressed && _slider.value != _maxHP)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _sliderSpeed * Time.deltaTime);

            if (_slider.value == _targetValue)
            {
                _healButtonPressed = false;
            }
        }

        if (_hurtButtonPressed && _slider.value != _minHP)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _sliderSpeed * Time.deltaTime);

            if (_slider.value == _targetValue)
            {
                _hurtButtonPressed = false;
            }
        }

    }

    public void ChangeHpValue()
    {
        if (TryGetComponent(out HealButton healButton))
        {
            _hurtButtonPressed = false;
            _healButtonPressed = true;
            _targetValue = _slider.value + _healPower;

            if (_targetValue > _maxHP)
            {
                _targetValue = _maxHP;
            }
        }

        if (TryGetComponent(out HurtButton hurtButton))
        {
            _healButtonPressed = false;
            _hurtButtonPressed = true;
            _targetValue = _slider.value + _hurtPower;

            if (_targetValue < _minHP)
            {
                _targetValue = _minHP;
            }
        }
    }
}
