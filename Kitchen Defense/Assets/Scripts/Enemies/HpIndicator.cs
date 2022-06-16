using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Enemy))]

public class HpIndicator : MonoBehaviour
{
    [SerializeField] private Color _targetColor;
   
    private Color _currentColor;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.IsHit += OnEnemyHit;
        _currentColor = Color.white;
    }

    public void OnEnemyHit(Enemy enemy)
    {
        enemy.EnemySprite.material.color = Color.Lerp(_targetColor,_currentColor, enemy.CurrentHealth/enemy.MaxHealth);
    }

    private void OnDestroy()
    {
        _enemy.IsHit -= OnEnemyHit;
    }
}
