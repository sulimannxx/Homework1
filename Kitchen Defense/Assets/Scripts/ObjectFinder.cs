using UnityEngine;
using UnityEngine.UI;

public class ObjectFinder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Toggle _soundToggle;

    public Player GetPlayer()
    {
        return _player;
    }

    public Transform GetCanvas()
    {
        return _canvas;
    }

    public Toggle GeToggle()
    {
        return _soundToggle;
    }
}
