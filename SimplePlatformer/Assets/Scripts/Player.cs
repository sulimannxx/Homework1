using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerAnimationController))]

public class Player : MonoBehaviour
{
    public bool IsDead { get; private set; }
    public int Coins { get; private set; }

    private PlayerAnimationController _animationController;

    private void Start()
    {
        _animationController = GetComponent<PlayerAnimationController>();
    }

    public void Die()
    {
        IsDead = true;
        _animationController.SetAnimatorInactive();
    }

    public void AddCoin()
    {
        Coins += 1;
        Debug.Log("Total coins: " + Coins);
    }
}
