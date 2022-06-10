using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Warehouse : Utility
{
    [SerializeField] private Player _player;

    private string _warehouseSkillName = "WarehouseCapacitySkill";

    private const int MaxLevel = 15;

    public int MaxCapacity { get; private set; } = 100;

    public UnityAction CapacityChanged;

    private void Start()
    {
        if (Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.WarehouseIsBought == true)
        {
            EnableWarehouse();
            IsBought = true;
        }
    }

    private void OnEnable()
    {
        Camera.main.GetComponent<ProgressSaveManager>().PlayerProfile.WarehouseIsBought = true;
        StartCoroutine(RecountCapacityAfterOneFrame());
    }

    private IEnumerator RecountCapacityAfterOneFrame()
    {
        yield return new WaitForEndOfFrame();
        RecountMaxCapacity();
    }

    private void RecountMaxCapacity()
    {
        Debug.Log(_player.SpellBook);
        switch (_player.SpellBook.GetSkillLevel(_warehouseSkillName))
        {
            case 1:
                MaxCapacity = 100;
                break;
            
            case 2:
                MaxCapacity = 500;
                break;
            
            case 3:
                MaxCapacity = 1700;
                break;

            case 4:
                MaxCapacity = 6000;
                break;

            case 5:
                MaxCapacity = 13000;
                break;

            case 6:
                MaxCapacity = 26000;
                break;

            case 7:
                MaxCapacity = 50000;
                break;

            case 8:
                MaxCapacity = 85000;
                break;

            case 9:
                MaxCapacity = 200000;
                break;

            case 10:
                MaxCapacity = 500000;
                break;

            case 11:
                MaxCapacity = 2000000;
                break;

            case 12:
                MaxCapacity = 10000000;
                break;

            case 13:
                MaxCapacity = 50000000;
                break;

            case 14:
                MaxCapacity = 200000000;
                break;

            case MaxLevel:
                MaxCapacity = 1000000000;
                break;

            default:
                MaxCapacity = 100;
                break;
        }

        CapacityChanged?.Invoke();
    }

    private void EnableWarehouse()
    {
        this.gameObject.SetActive(true);
    }

    public override bool Bought(bool state)
    {
        if (state == true && _player.SpellBook.GetSkillLevel(_warehouseSkillName) < MaxLevel)
        {
            EnableWarehouse();
            _player.SpellBook.IncreaseSkillLevel(_warehouseSkillName);
            RecountMaxCapacity();

            if (_player.SpellBook.GetSkillLevel(_warehouseSkillName) == MaxLevel)
            {
                return IsBought = true;
            }

            return IsBought = false;
        }
        else if (state == true && _player.SpellBook.GetSkillLevel(_warehouseSkillName) == MaxLevel)
        {
            return IsBought = true;
        }
        else
        {
            return IsBought = false;
        }
    }

    public override void Init()
    {
        Start();
    }
}
