using UnityEngine;

public class GetCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
