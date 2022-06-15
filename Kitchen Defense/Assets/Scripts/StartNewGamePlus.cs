using UnityEngine;

public class StartNewGamePlus : MonoBehaviour
{
    [SerializeField] private ProgressSaveManager _progressSaveManager;

    public void OnButtonClick()
    {
        if (WaveController.GameWave >= 100)
        {
            _progressSaveManager.ResetProfile();
        }
    }
}
