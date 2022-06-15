using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ProgressSaveManager : MonoBehaviour
{
    public PlayerData PlayerProfile;
    public Utility[] Utilities;
    public Sprite[] Skins;
    public BuyGloveButton[] BuyGloveButtons;
    public BuyWeaponButton[] BuyWeaponButtons;

    private void Start()
    {
        LoadGame();

        foreach (var utility in Utilities)
        {
            utility.Init();
        }

        foreach (var id in PlayerProfile.BoughtSkinsId)
        {
            foreach (var button in BuyGloveButtons)
            {
                button.LoadButtonState(id);
            }
        }

        foreach (var id in PlayerProfile.BoughtWeaponsId)
        {
            foreach (var button in BuyWeaponButtons)
            {
                button.LoadButtonState(id);
            }
        }
    }

    public void ResetProfile()
    {
        for (int i = 0; i < PlayerProfile.PlayerSkillLevels.Count; i++)
        {
            PlayerProfile.PlayerSkillLevels[i] = 1;
        }

        PlayerProfile.WarehouseIsBought = false;
        PlayerProfile.SugarFarmIsBought = false;
        PlayerProfile.IceCreamIsBought = false;
        PlayerProfile.Money = 0;
        PlayerProfile.CurrentSkinId = 0;
        PlayerProfile.MoneyIncomeBonus += WaveController.GameWave / 10f;
        PlayerProfile.GameWave = 1;
        PlayerProfile.BoughtSkinsId.Clear();
        PlayerProfile.BoughtWeaponsId.Clear();
        Time.timeScale = 1;
        SaveGame();
        SceneManager.LoadScene("MainGame");
    }

    public void SaveSkills(int value)
    {
        PlayerProfile.PlayerSkillLevels.Add(value);
    }

    public void SaveGame()
    {
        //File.WriteAllText(Application.streamingAssetsPath + "/save.json", JsonUtility.ToJson(PlayerProfile));
        PlayerPrefs.SetString("save", JsonUtility.ToJson(PlayerProfile));
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("save"))//(File.Exists(Application.streamingAssetsPath + "/save.json"))
        {
            PlayerProfile = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("save"));//(File.ReadAllText(Application.streamingAssetsPath + "/save.json"));
            WaveController.GameWave = PlayerProfile.GameWave;

            if (WaveController.GameWave < 1)
            {
                WaveController.GameWave = 1;
            }

            if (PlayerProfile.MoneyIncomeBonus < 1f)
            {
                PlayerProfile.MoneyIncomeBonus = 1;
            }
        }
    }

    public float[] LoadCurrentSkinIdStats()
    {
        float[] tempStats = new float[4];

        if (PlayerProfile.CurrentSkinId == 0)
        {
            PlayerProfile.CurrentSkinId = 1;
        }

        switch (PlayerProfile.CurrentSkinId)
        {
            case 1:
                tempStats[0] = 1;
                tempStats[1] = 0;
                tempStats[2] = 0;
                tempStats[3] = 0;
                return tempStats;
            case 2:
                tempStats[0] = 1.005f;
                tempStats[1] = 0.5f;
                tempStats[2] = 0;
                tempStats[3] = 0;
                return tempStats;
            case 3:
                tempStats[0] = 1.01f;
                tempStats[1] = 0.6f;
                tempStats[2] = 0;
                tempStats[3] = 0;
                return tempStats;
            case 4:
                tempStats[0] = 1.03f;
                tempStats[1] = 1;
                tempStats[2] = 0;
                tempStats[3] = 0;
                return tempStats;
            case 5:
                tempStats[0] = 1.035f;
                tempStats[1] = 1.5f;
                tempStats[2] = 0;
                tempStats[3] = 0;
                return tempStats;
            case 6:
                tempStats[0] = 1.04f;
                tempStats[1] = 2;
                tempStats[2] = 0;
                tempStats[3] = 0;
                return tempStats;
            case 7:
                tempStats[0] = 1.06f;
                tempStats[1] = 3;
                tempStats[2] = 0.05f;
                tempStats[3] = 0;
                return tempStats;
            case 8:
                tempStats[0] = 1.07f;
                tempStats[1] = 4;
                tempStats[2] = 0.1f;
                tempStats[3] = 0;
                return tempStats;
            case 9:
                tempStats[0] = 1.08f;
                tempStats[1] = 5;
                tempStats[2] = 0.15f;
                tempStats[3] = 0;
                return tempStats;
            case 10:
                tempStats[0] = 1.12f;
                tempStats[1] = 10;
                tempStats[2] = 0.3f;
                tempStats[3] = 0;
                return tempStats;
            case 11:
                tempStats[0] = 1.15f;
                tempStats[1] = 12;
                tempStats[2] = 0.4f;
                tempStats[3] = 0;
                return tempStats;
            case 12:
                tempStats[0] = 1.2f;
                tempStats[1] = 15;
                tempStats[2] = 0.5f;
                tempStats[3] = 1;
                return tempStats;
        }

        return tempStats;
    }

    public Sprite LoadCurrentSkinSprite(int id)
    {
        return Skins[id];
    }

    [System.Serializable]
    public class PlayerData
    {
        public List<int> PlayerSkillLevels;
        public bool WarehouseIsBought;
        public bool SugarFarmIsBought;
        public bool IceCreamIsBought;
        public float MoneyIncomeBonus;
        public int GameWave;
        public int Money;
        public int Pies;
        public float CurrentHealth;
        public int CurrentSkinId;
        public List<int> BoughtSkinsId;
        public List<int> BoughtWeaponsId;
    }
}
