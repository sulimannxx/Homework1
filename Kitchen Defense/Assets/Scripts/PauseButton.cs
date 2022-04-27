using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Image _blackFilter;
    public void PressPauseButton()
    {
        if (_blackFilter.IsActive() == true)
        {
            _blackFilter.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if (_blackFilter.IsActive() == false)
        {
            Time.timeScale = 0;
            _blackFilter.gameObject.SetActive(true);
        }
    }
}
