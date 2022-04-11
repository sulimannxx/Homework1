using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _playerAnimator; 

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();

    }
    public void SetAnimatorInactive()
    {
        _playerAnimator.enabled = false;
    }

    public void SetBool(string name, bool state)
    {
        _playerAnimator.SetBool(name, state);
    }

    public void SetFloat(string name, float number)
    {
        _playerAnimator.SetFloat(name, number);
    }
}
