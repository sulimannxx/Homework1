using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SkillBook))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private Shop _shop;
    [SerializeField] private CloseInfoWindow _closeInfoWindow;
    [SerializeField] private NextWaveButton _nextWaveButton;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private GameOverButton _repeatWaveButton;
    [SerializeField] private GameOverButton _previousWaveButton;
    [SerializeField] private AudioSource _bulletThrowAudioSource;
    [SerializeField] private AudioClip[] _shootSounds;
    [SerializeField] private AudioSource _playerHitAudioSource;
    [SerializeField] private AudioClip[] _playerHitSounds;
    [SerializeField] private AudioSource _playerHealtAudioSource;
    [SerializeField] private ProgressSaveManager _progressSaveManager;

    private int _currentWeaponIndex;
    private Animator _animator;
    private string _playerAttackAnimation = "Attack";
    private string _criticalStrikeSkill = "CriticalStrikeSkill";
    private string _pieChanceSkill = "PieChanceSkill";
    private string _healthSkill = "HealthSkill";
    private bool _readyToShoot = true;
    private SolidWall _solidWall;
    private Aura _aura;
    private float _maxCriticalStrikeChance = 100;
    private float _maxPieReceiveChance = 100;
    private float[] _loadGloveStatsData;

    public float GloveDamageModifier { get; private set; } = 1;
    public float CriticalStrikeModifier { get; private set; } = 1;
    public float WeaponCoolDownModifier { get; private set; } = 0;
    public float PieChanceModifier { get; private set; } = 0;
    public SkillBook SpellBook { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float Money { get; private set; }
    public float PieCoins { get; private set; }
    public float EarnedMoneyOnThisWave { get; private set; }
    public float EarnedPiesOnThisWave { get; private set; }
    public Weapon CurrentWeapon { get; private set; }
    public bool AuraIsActivated { get; private set; } = false;

    public event UnityAction PlayerIsDead;
    public event UnityAction HealthIsChanged;
    public event UnityAction MoneyChanged;
    public event UnityAction HasMoreThanOneWeapon;
    public event UnityAction WeaponChanged;
    public event UnityAction PieCoinReceived;
    public event UnityAction GloveBuyButtonPressed;

    private void Start()
    {
        CurrentWeapon = _weaponList[0];
        _currentWeaponIndex = 0;
        _animator = GetComponent<Animator>();
        _shop.ShopUiButtonPressed += OnUiButtonPressed;
        _closeInfoWindow.InfoButtonClosePressed += OnUiButtonPressed;
        _nextWaveButton.WaveUiButtonPressed += OnUiButtonPressed;
        _previousWaveButton.WaveUiButtonPressed += OnUiButtonPressed;
        _repeatWaveButton.WaveUiButtonPressed += OnUiButtonPressed;
        SpellBook = GetComponent<SkillBook>();
        _aura = GetComponentInChildren<Aura>();
        _aura.gameObject.SetActive(false);
        RecountMaxHealth();
        CurrentWeapon.Init(this);
        StartCoroutine(LoadMoneyAfterOneFrame());
        _loadGloveStatsData = _progressSaveManager.LoadCurrentSkinIdStats();
        LoadGloveStats();
        LoadGloveSprite();
        LoadCurrentHealth();

        if (_progressSaveManager.PlayerProfile.MoneyIncomeBonus < 1)
        {
            _progressSaveManager.PlayerProfile.MoneyIncomeBonus = 1;
        }
    }

    private void OnEnable()
    {
        foreach (var weapon in _weaponList)
        {
            StartCoroutine(WeaponCooldown(weapon));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsPointerOverUIObject() == false &&
            _readyToShoot == true)
        {
            if (CurrentWeapon.CoolDownCurrentValue <= 0)
            {
                _bulletThrowAudioSource.clip = _shootSounds[Random.Range(0, _shootSounds.Length)];
                _bulletThrowAudioSource.Play();
                _animator.Play(_playerAttackAnimation);
                CurrentWeapon.Shoot(this);
                CurrentWeapon.SetCurrentCoolDownValue(CurrentWeapon.CoolDownBaseValue);
                StartCoroutine(WeaponCooldown(CurrentWeapon));
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private IEnumerator LoadMoneyAfterOneFrame()
    {
        yield return new WaitForEndOfFrame();
        Money = _progressSaveManager.PlayerProfile.Money;
        PieCoins = _progressSaveManager.PlayerProfile.Pies;
        MoneyChanged?.Invoke();
    }

    private IEnumerator LoadWeaponInventoryAfterOneFrame(Weapon weapon)
    {
        yield return new WaitForEndOfFrame();
        _weaponList.Add(weapon);
         _weaponList[_weaponList.Count - 1].Init(this);
        HasMoreThanOneWeapon?.Invoke();
    }

    private IEnumerator WeaponCooldown(Weapon weapon)
    {
        while (weapon.CoolDownCurrentValue > 0)
        {
            weapon.SetCurrentCoolDownValue(weapon.CoolDownCurrentValue - Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator AuraCountdown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _aura.gameObject.SetActive(false);
        AuraIsActivated = false;
    }

    public void LoadCurrentHealth()
    {
        CurrentHealth = _progressSaveManager.PlayerProfile.CurrentHealth;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = MaxHealth;
        }

        HealthIsChanged?.Invoke();
    }

    public void LoadWeaponInventory(Weapon weapon)
    {
        StartCoroutine(LoadWeaponInventoryAfterOneFrame(weapon));
    }

    public void DecreasePies(int pies)
    {
        PieCoins -= pies;
        MoneyChanged?.Invoke();
        _progressSaveManager.PlayerProfile.Pies = (int)PieCoins;
    }

    public void DecreaseMoneyAndPies(int money, int pies)
    {
        Money -= money;
        PieCoins -= pies;
        MoneyChanged?.Invoke();
        _progressSaveManager.PlayerProfile.Pies = (int)PieCoins;
        _progressSaveManager.PlayerProfile.Money = (int)Money;
    }

    public Weapon GetWeapon<T>()
    {
        foreach (var weapon in _weaponList)
        {
            if (weapon is T)
            {
                return weapon;
            }
        }
        return null;
    }

    public void ApplyDamage(float damage)
    {
        if (_solidWall.IsSpawned == false)
        {
            if (AuraIsActivated == false)
            {
                _playerHitAudioSource.clip = _playerHitSounds[Random.Range(0, _playerHitSounds.Length)];
                _playerHitAudioSource.Play();
                CurrentHealth -= damage;
                HealthIsChanged?.Invoke();
                _progressSaveManager.PlayerProfile.CurrentHealth = CurrentHealth;

                if (CurrentHealth <= 0)
                {
                    PlayerIsDead?.Invoke();

                    if (this.gameObject != null)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (_solidWall.IsSpawned == true)
        {
            _solidWall.ApplyDamage(damage);
        }
    }

    public void Heal(float health)
    {
        if (CurrentHealth + health <= MaxHealth)
        {
            _playerHealtAudioSource.Play();
            CurrentHealth += health;
            HealthIsChanged?.Invoke();
            _progressSaveManager.PlayerProfile.CurrentHealth = CurrentHealth;
        }
        else if (CurrentHealth + health >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            HealthIsChanged?.Invoke();
            _progressSaveManager.PlayerProfile.CurrentHealth = CurrentHealth;
        }
    }

    public void ResetHealthAndEnablePlayer()
    {
        CurrentHealth = MaxHealth;
        gameObject.SetActive(true);
        HealthIsChanged?.Invoke();
        _progressSaveManager.PlayerProfile.CurrentHealth = CurrentHealth;
    }

    public void ActivateAura(float seconds, Sprite sprite)
    {
        AuraIsActivated = true;
        _aura.gameObject.SetActive(true);
        _aura.GetComponent<SpriteRenderer>().sprite = sprite;
        StartCoroutine(AuraCountdown(seconds));
    }

    public void HealByItem(float health)
    {
        _playerHealtAudioSource.Play();
        CurrentHealth += health;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        HealthIsChanged?.Invoke();
        _progressSaveManager.PlayerProfile.CurrentHealth = CurrentHealth;
    }

    public bool CriticalStrikeChance()
    {
        float chance = Random.Range(0, _maxCriticalStrikeChance);

        if (chance <= SpellBook.GetSkillLevel(_criticalStrikeSkill) + CriticalStrikeModifier)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InitSolidWall(SolidWall solidWall)
    {
        _solidWall = solidWall;
    }

    public void SetPlayerSkin(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetBonusStatsDefaultValues()
    {
        GloveDamageModifier = 1;
        CriticalStrikeModifier = 0;
        WeaponCoolDownModifier = 0;
        PieChanceModifier = 0;
    }

    public void AddGreyGloveBonusStats(float damage, float crit)
    {
        GloveDamageModifier = damage;
        CriticalStrikeModifier = crit;
    }

    public void AddBlueGloveBonusStats(float damage, float crit, float cooldown)
    {
        GloveDamageModifier = damage;
        CriticalStrikeModifier = crit;
        WeaponCoolDownModifier = cooldown;
        GloveBuyButtonPressed?.Invoke();
    }

    public void AddGoldenGloveBonusStats(float damage, float crit, float cooldown, float chance)
    {
        GloveDamageModifier = damage;
        CriticalStrikeModifier = crit;
        WeaponCoolDownModifier = cooldown;
        PieChanceModifier = chance;
        GloveBuyButtonPressed?.Invoke();
    }

    public void AddMoney(float reward)
    {
        Money += reward * _progressSaveManager.PlayerProfile.MoneyIncomeBonus;
        EarnedMoneyOnThisWave += reward;
        Money = 9999999;

        float chance = Random.Range(0, _maxPieReceiveChance);

        if (chance <= (float)SpellBook.GetSkillLevel(_pieChanceSkill) / 35f + 1f + PieChanceModifier)
        {
            PieCoins++;
            EarnedPiesOnThisWave++;
            PieCoinReceived?.Invoke();
            _progressSaveManager.PlayerProfile.Pies = (int)PieCoins;
        }

        if (Money + reward > _warehouse.MaxCapacity)
        {
            Money = _warehouse.MaxCapacity;
        }

        _progressSaveManager.PlayerProfile.Money = (int)Money;
        MoneyChanged?.Invoke();
    }

    public void AddMoneyFromShop(float money)
    {
        Money += money;

        if (Money + money > _warehouse.MaxCapacity)
        {
            Money = _warehouse.MaxCapacity;
        }

        MoneyChanged?.Invoke();
        _progressSaveManager.PlayerProfile.Money = (int)Money;
    }

    public void AddPieFromShop(float pies)
    {
        PieCoins += pies;
        MoneyChanged?.Invoke();
        _progressSaveManager.PlayerProfile.Pies = (int)PieCoins;
    }

    public void DoubleEarnedMoneyOnWave(float reward)
    {
        Money += reward;

        if (Money + reward > _warehouse.MaxCapacity)
        {
            Money = _warehouse.MaxCapacity;
        }

        MoneyChanged?.Invoke();
        _progressSaveManager.PlayerProfile.Money = (int)Money;
    }

    public void RecountMaxHealth()
    {
        MaxHealth = Mathf.Pow(SpellBook.GetSkillLevel(_healthSkill), 3) + 50;
        HealthIsChanged?.Invoke();
    }

    public void ResetEarnedOnThisWaveValue()
    {
        EarnedMoneyOnThisWave = 0;
        EarnedPiesOnThisWave = 0;
    }

    public void AddWeaponToInventory(Weapon weapon, int price)
    {
        _weaponList.Add(weapon);
        _weaponList[_weaponList.Count - 1].Init(this);
        Money -= price;
        MoneyChanged?.Invoke();
        HasMoreThanOneWeapon?.Invoke();
        _progressSaveManager.PlayerProfile.Money = (int)Money;
    }

    public void DecreaseMoney(int money)
    {
        Money -= money;
        MoneyChanged?.Invoke();
        _progressSaveManager.PlayerProfile.Money = (int)Money;
    }

    public void SwitchWeapon(int index)
    {

        _currentWeaponIndex += index;

        if (_currentWeaponIndex >= _weaponList.Count)
        {
            _currentWeaponIndex = 0;
            CurrentWeapon = _weaponList[_currentWeaponIndex];
        }
        else if (_currentWeaponIndex < 0)
        {

            _currentWeaponIndex = _weaponList.Count - 1;
            CurrentWeapon = _weaponList[_currentWeaponIndex];
        }
        else if (_currentWeaponIndex >= 0 && _currentWeaponIndex < _weaponList.Count)
        {
            CurrentWeapon = _weaponList[_currentWeaponIndex];
        }

        WeaponChanged?.Invoke();
    }

    public void SetPlayerShootAbility(bool state)
    {
        _readyToShoot = state;
    }

    public void OnUiButtonPressed(bool state)
    {
        if (state == true)
        {
            _readyToShoot = false;
        }
        else if (state == false)
        {
            _readyToShoot = true;
        }
    }

    private void LoadGloveStats()
    {
        GloveDamageModifier = _loadGloveStatsData[0];
        CriticalStrikeModifier = _loadGloveStatsData[1];
        WeaponCoolDownModifier = _loadGloveStatsData[2];
        PieChanceModifier = _loadGloveStatsData[3];
    }

    private void LoadGloveSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _progressSaveManager.LoadCurrentSkinSprite(_progressSaveManager.PlayerProfile.CurrentSkinId - 1);
    }

    private void OnDestroy()
    {
        _shop.ShopUiButtonPressed -= OnUiButtonPressed;
        _nextWaveButton.WaveUiButtonPressed -= OnUiButtonPressed;
        _previousWaveButton.WaveUiButtonPressed -= OnUiButtonPressed;
        _repeatWaveButton.WaveUiButtonPressed -= OnUiButtonPressed;
    }
}
