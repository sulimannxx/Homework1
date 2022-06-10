using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponInfoButton : MonoBehaviour
{
    [SerializeField] private Image _mainImage;
    [SerializeField] private Sprite _mainSprite;
    [SerializeField] private Sprite _infoSprite;
    [SerializeField] private GameObject _icon;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _weaponInfoText;
    [SerializeField] private GameObject _purchaseButton;
    [SerializeField] private GameObject _moneyIcon;
    [SerializeField] private AudioSource _audioSource;

    private bool _isMainImageOn = true;
    private bool _stopRotation;

    public UnityAction<bool> InfoButtonPressed;

    public void ShowWeaponInfo()
    {
        if (_isMainImageOn == true)
        {
            StartCoroutine(FlipMainInfo(_mainImage, _infoSprite, false));
            InfoButtonPressed?.Invoke(false);
        }
        else if (_isMainImageOn == false)
        {
            StartCoroutine(FlipMainInfo(_mainImage, _mainSprite, true));
            InfoButtonPressed?.Invoke(true);
        }
    }

    private IEnumerator FlipMainInfo(Image currentImage, Sprite currentSprite, bool state)
    {
        _audioSource.Play();
        _stopRotation = false;

        while (currentImage.transform.localScale.x > 0.05f && _stopRotation == false)
        {
            currentImage.transform.localScale = new Vector3(currentImage.transform.localScale.x - Time.unscaledDeltaTime * 5, 1, 1);

            if (currentImage.transform.localScale.x <= 0.065f)
            {
                currentImage.sprite = currentSprite;
                _icon.SetActive(state);
                _text.SetActive(state);
                _purchaseButton?.SetActive(state);
                if(_moneyIcon != null)_moneyIcon.SetActive(state);
                _weaponInfoText.SetActive(!state);

                while (currentImage.transform.localScale.x < 0.95f)
                {
                    currentImage.transform.localScale = new Vector3(currentImage.transform.localScale.x + Time.unscaledDeltaTime * 5, 1, 1);

                    if (currentImage.transform.localScale.x > 0.95f)
                    {
                        currentImage.transform.localScale = new Vector3(1, 1, 1);

                        _stopRotation = true;
                    }

                    yield return null;
                }
            }
            
            _isMainImageOn = state;
            yield return null;
        }
    }
}
