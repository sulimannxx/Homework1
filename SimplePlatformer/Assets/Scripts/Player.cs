using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public bool IsDead { get; private set; }
    public int Coins { get; private set; }

    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void SetDead()
    {
        IsDead = true;
    }

    public void SetAnimatorInactive()
    {
        _playerAnimator.enabled = false;
    }

    public void AddCoin()
    {
        Coins += 1;
        Debug.Log("Total coins: " + Coins);
    }
}
