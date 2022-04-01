using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    private RaycastHit2D _hit;

    private void Update()
    {
        _hit = Physics2D.Raycast(_shootPoint.position, Vector2.right);
        Debug.DrawRay(_shootPoint.position, Vector2.right * 10, Color.red);
        if (Input.GetKey(KeyCode.Mouse0) && _hit == true)
        {
            Destroy(_hit.collider.gameObject);
        }
    }
}
