using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponIcon : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Image _sprite;

    private void Start()
    {
        _player.WeaponChanged += OnWeaponChanged;
        _sprite = GetComponent<Image>();
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        _sprite.fillAmount = 1 - (_player.CurrentWeapon.CoolDownCurrentValue / _player.CurrentWeapon.CoolDownBaseValue);
    }

    private void OnWeaponChanged()
    {
        _sprite.sprite = _player.CurrentWeapon.GetComponent<SpriteRenderer>().sprite;
    }
}
