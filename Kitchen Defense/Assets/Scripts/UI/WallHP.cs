using UnityEngine;
using UnityEngine.UI;

public class WallHP : MonoBehaviour
{
    [SerializeField] private SolidWall _wall;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;

    private Color _colorGreen = Color.green;
    private Color _colorRed = Color.red;

    private void Start()
    {
        _wall.WallIsHit += ChangeSliderHpValue;
    }

    private void ChangeSliderHpValue()
    {
        _slider.value = _wall.CurrentHealth / _wall.MaxHealth;
        _image.color = Color.Lerp(_colorRed, _colorGreen, _wall.CurrentHealth / _wall.MaxHealth);
    }

    private void OnDestroy()
    {
        _wall.WallIsHit -= ChangeSliderHpValue;
    }
}
