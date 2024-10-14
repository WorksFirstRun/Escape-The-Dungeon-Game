using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private int goldenKeysNumber;
    private int silverKeysNubmber;
    public event Action<int> updateUiForGoldenKeys;
    public event Action<int> updateUiForSilverKeys;

    private void Awake()
    {
        Instance = this;
        LoadKeys();
    }
    
    public static PlayerPickUp Instance { get; private set; }

    public void ResetKeys()
    {
        goldenKeysNumber = 0;
        silverKeysNubmber = 0;
        SaveKeysAmount();
        updateUiForGoldenKeys?.Invoke(goldenKeysNumber);
        updateUiForSilverKeys?.Invoke(silverKeysNubmber);
    }
    
    
    public void AddGoldenKey()
    {
        goldenKeysNumber++;
        updateUiForGoldenKeys?.Invoke(goldenKeysNumber);
        SaveKeysAmount();
    }

    public void TakeGoldenKey()
    {
        if (goldenKeysNumber > 0)
        {
            goldenKeysNumber--;
            updateUiForGoldenKeys?.Invoke(goldenKeysNumber);
            SaveKeysAmount();
        }
    }

    public void AddSilverKey()
    {
        silverKeysNubmber++;
        updateUiForSilverKeys?.Invoke(silverKeysNubmber);
        SaveKeysAmount();
    }

    public void TakeSilverKey()
    {
        if (silverKeysNubmber > 0)
        {
            silverKeysNubmber--;
            updateUiForSilverKeys?.Invoke(silverKeysNubmber);
            SaveKeysAmount();
        }
    }

    public int GetSilverKeysNumber()
    {
        return silverKeysNubmber;
    }

    private void SaveKeysAmount()
    {
        PlayerPrefs.SetInt("GKey", goldenKeysNumber);
        PlayerPrefs.SetInt("SKey", silverKeysNubmber);
        PlayerPrefs.Save();
    }

    private void LoadKeys()
    {
        if (PlayerPrefs.HasKey("GKey"))
        {
            goldenKeysNumber = PlayerPrefs.GetInt("GKey");
        }
        if (PlayerPrefs.HasKey("SKey"))
        {
            silverKeysNubmber = PlayerPrefs.GetInt("SKey");
        }
        updateUiForGoldenKeys?.Invoke(goldenKeysNumber);
        updateUiForSilverKeys?.Invoke(silverKeysNubmber);
    }
    
    public int GetGoldenKeysNumber()
    {
        return goldenKeysNumber;
    }

}
