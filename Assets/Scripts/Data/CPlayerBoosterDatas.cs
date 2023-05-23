using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BoosterType
{
    LEVEL = 0,
    EXP = 1,
    ENERGY = 2,
    COIN = 3,
    GAMEDOT = 4
}

[System.Serializable]
public class CBoosterDataCommodity
{
    public BoosterType type;
    public long value;

    private CBoosterDataCommodity() { }

    public CBoosterDataCommodity(BoosterType type, long value)
    {
        this.type = type;
        this.value = value;
    }

    public void AddValue(long value)
    {
        this.value += value;
    }

    public void SubtractValue(long value)
    {
        this.value = (long)Mathf.Clamp(this.value - value, 0, this.value);
    }

    public void SetValue(long value)
    {
        this.value = value;
    }
}

[System.Serializable]
public class CPlayerBoosterDatas
{
    public static CPlayerBoosterDatas Instance => CGameDataManager.Instance._gameData?._playerBoosterDatas;

    public List<CBoosterDataCommodity> _playerBoosterDatas;
    public Dictionary<BoosterType, CBoosterDataCommodity> _dictionaryPlayerBooster;

    private const int INITIAL_LEVEL = 1;
    private const int INITIAL_EXP = 0;
    private const int INITIAL_ENERGY = 5;
    private const int INITIAL_COINS = 500;
    private const int INITIAL_GAMEDOTS = 0;

    public UnityAction<BoosterType> _callbackOnBoosterUpdated; 

    public void OpenApp()
    {
        this.SetUpDictionary();
    }

    private void SetUpDictionary()
    {
        this._dictionaryPlayerBooster = new Dictionary<BoosterType, CBoosterDataCommodity>();

        foreach(CBoosterDataCommodity boosterData in this._playerBoosterDatas)
        {
            if (!this._dictionaryPlayerBooster.ContainsKey(boosterData.type))
            {
                this._dictionaryPlayerBooster.Add(boosterData.type, boosterData);
            }
        }
    }

    public void CreateNew()
    {
        this._playerBoosterDatas = new List<CBoosterDataCommodity>();

        this._playerBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.LEVEL, value: INITIAL_LEVEL));
        this._playerBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.EXP, value: INITIAL_EXP));
        this._playerBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.ENERGY, value: INITIAL_ENERGY));
        this._playerBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.COIN, value: INITIAL_COINS));
        this._playerBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.GAMEDOT, value: INITIAL_GAMEDOTS));
    }

    public void AddNewBooster(BoosterType type, long value)
    {
        if (this._playerBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryPlayerBooster == null)
        {
            this.SetUpDictionary();
        }

        if (!this._dictionaryPlayerBooster.ContainsKey(type))
        {
            CBoosterDataCommodity newBooster = new CBoosterDataCommodity(type, value);
            this._dictionaryPlayerBooster.Add(type, newBooster);
        }
        else
        {
            Debug.LogError($"EXISTED BOOSTER {type}");
        }
    }

    public void AddValueBooster(BoosterType type, long value)
    {
        if (this._playerBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryPlayerBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryPlayerBooster.TryGetValue(type, out CBoosterDataCommodity booster))
        {
            booster.AddValue(value);
            this.OnBoosterUpdatedCallback(type);
        }
        else
        {
            Debug.LogError($"NOT EXISTED BOOSTER {type}");
        }
    }

    public void SubtractValueBooster(BoosterType type, long value)
    {
        if (this._playerBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryPlayerBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryPlayerBooster.TryGetValue(type, out CBoosterDataCommodity booster))
        {
            booster.SubtractValue(value);
            this.OnBoosterUpdatedCallback(type);
        }
        else
        {
            Debug.LogError($"NOT EXISTED BOOSTER {type}");
        }
    }

    public void SetValueBooster(BoosterType type, long value)
    {
        if (this._playerBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryPlayerBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryPlayerBooster.TryGetValue(type, out CBoosterDataCommodity booster))
        {
            booster.SetValue(value);
            this.OnBoosterUpdatedCallback(type);
        }
        else
        {
            Debug.LogError($"NOT EXISTED BOOSTER {type}");
        }
    }

    public long GetBoosterValue(BoosterType type)
    {
        if (this._playerBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryPlayerBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryPlayerBooster.TryGetValue(type, out CBoosterDataCommodity booster))
        {
            return booster.value;
        }
        else
        {
            Debug.LogError($"NOT EXISTED BOOSTER {type}");
            return -1;
        }
    }

    public void AssignCallbackOnBoosterUpdated(UnityAction<BoosterType> callback)
    {
        this._callbackOnBoosterUpdated -= callback;
        this._callbackOnBoosterUpdated += callback;
    }

    public void UnAssignCallbackOnBoosterUpdated(UnityAction<BoosterType> callback)
    {
        this._callbackOnBoosterUpdated -= callback;
    }

    public void OnBoosterUpdatedCallback(BoosterType type)
    {
        this._callbackOnBoosterUpdated?.Invoke(type);
    }
}
