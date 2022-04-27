using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private int _nextWeapon = 1;
    private int _previousWeapon = -1;
    private Color _blockedColor = new Color(0.1603774f, 0.1603774f, 0.1603774f, 1);

    private void Start()
    {
        _player.HasMoreThanOneWeapon += EnableSwitchWeaponsButtons;
        _image.color = _blockedColor;
        _button.enabled = false;
    }

    private void EnableSwitchWeaponsButtons()
    {
        _button.enabled = true;
        _image.color = Color.white;
    }

    public void ButtonPressed()
    {
        if (TryGetComponent(out NextWeaponButton nextWeaponButton))
        {
            _player.SwitchWeapon(_nextWeapon);
        }
        else if (TryGetComponent(out PreviousWeaponButton previousWeaponButton))
        {
            _player.SwitchWeapon(_previousWeapon);
        }
    }

    
}
