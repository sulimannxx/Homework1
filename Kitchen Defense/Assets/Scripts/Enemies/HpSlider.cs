using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    [SerializeField] private EnemyHpBarPosition enemyHpBarPosition;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;

    private Color _colorGreen = Color.green;
    private Color _colorRed = Color.red;

    private void Start()
    {
        _enemy.IsHit += ChangeSliderHpValue;
    }

    private void Update()
    {
        transform.position = enemyHpBarPosition.transform.position;
    }

    private void ChangeSliderHpValue(Enemy enemy)
    {
        _slider.value = enemy.CurrentHealth / enemy.MaxHealth;
        _image.color = Color.Lerp(_colorRed, _colorGreen, enemy.CurrentHealth / enemy.MaxHealth);
    }

    private void OnDestroy()
    {
        _enemy.IsHit -= ChangeSliderHpValue;
    }
}
