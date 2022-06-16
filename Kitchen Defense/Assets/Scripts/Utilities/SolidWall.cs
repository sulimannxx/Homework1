using UnityEngine;
using UnityEngine.Events;

public class SolidWall : Utility
{
    [SerializeField] private GameObject _wallDestroyEffect;
    [SerializeField] private SolidWall _solidWall;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _solidWallDestroyEffectPoint;
    [SerializeField] private AudioSource _playerHitAudioSource;
    [SerializeField] private AudioClip[] _playerHitSounds;

    private float _health;

    public float CurrentHealth => _health;

    public float MaxHealth { get; private set; }

    public bool IsSpawned { get; private set; }

    public UnityAction WallIsHit;

    private void Start()
    {
        ResetWall();
        IsSpawned = false;
        _solidWall.gameObject.SetActive(false);
        _player.InitSolidWall(_solidWall);
    }

    private void EnableWall()
    {
        _solidWall.gameObject.SetActive(true);
        IsSpawned = true;
        ResetWall();
    }

    private void ResetWall()
    {
        _health = 4 + WaveController.GameWave * 2;
        MaxHealth = _health;
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
        _playerHitAudioSource.clip = _playerHitSounds[Random.Range(0, _playerHitSounds.Length)];
        _playerHitAudioSource.Play();
        WallIsHit?.Invoke();

        if (_health <= 0)
        {
            IsBought = false;
            _wallDestroyEffect = Instantiate(_wallDestroyEffect, _solidWallDestroyEffectPoint);
            _solidWall.gameObject.SetActive(false);
            IsSpawned = false;
        }
    }

    public override bool EnableUtility(bool state)
    {
        if (state == true)
        {
            EnableWall();
        }

        return IsBought = state;
    }

    public override void Init()
    {}
}
