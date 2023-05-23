using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BoosterType
{
    EXP = 0,
    ENERGY = 1,
    COIN = 2,
    GAMEDOT = 3
}

public enum BoosterUpdateType
{
    ADD = 0,
    SUBTRACT = 1,
    SET = 2
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
    public List<CBoosterDataCommodity> _userBoosterDatas;
    public Dictionary<BoosterType, CBoosterDataCommodity> _dictionaryUserBooster;

    public const int INITIAL_EXP = 0;
    public const int INITIAL_ENERGY = 5;
    public const int INITIAL_COINS = 500;
    public const int INITIAL_GAMEDOTS = 0;

    public static CPlayerBoosterDatas Instance => CGameDataManager.Instance._gameData?._playerBoosterDatas;

    public UnityAction<BoosterType> _callbackOnBoosterUpdated; 

    public void OpenApp()
    {
        this.SetUpDictionary();
    }

    private void SetUpDictionary()
    {
        this._dictionaryUserBooster = new Dictionary<BoosterType, CBoosterDataCommodity>();

        foreach(CBoosterDataCommodity boosterData in this._userBoosterDatas)
        {
            if (!this._dictionaryUserBooster.ContainsKey(boosterData.type))
            {
                this._dictionaryUserBooster.Add(boosterData.type, boosterData);
            }
        }
    }

    public void CreateNew()
    {
        this._userBoosterDatas = new List<CBoosterDataCommodity>();

        this._userBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.EXP, value: INITIAL_EXP));
        this._userBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.ENERGY, value: INITIAL_ENERGY));
        this._userBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.COIN, value: INITIAL_COINS));
        this._userBoosterDatas.Add(new CBoosterDataCommodity(type: BoosterType.GAMEDOT, value: INITIAL_GAMEDOTS));
    }

    public void AddNewBooster(BoosterType type, long value)
    {
        if (this._userBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryUserBooster == null)
        {
            this.SetUpDictionary();
        }

        if (!this._dictionaryUserBooster.ContainsKey(type))
        {
            CBoosterDataCommodity newBooster = new CBoosterDataCommodity(type, value);
            this._dictionaryUserBooster.Add(type, newBooster);
        }
        else
        {
            Debug.LogError($"EXISTED BOOSTER {type}");
        }
    }

    public void AddValueBooster(BoosterType type, long value)
    {
        if (this._userBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryUserBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryUserBooster.TryGetValue(type, out CBoosterDataCommodity booster))
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
        if (this._userBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryUserBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryUserBooster.TryGetValue(type, out CBoosterDataCommodity booster))
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
        if (this._userBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryUserBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryUserBooster.TryGetValue(type, out CBoosterDataCommodity booster))
        {
            booster.SetValue(value);
            this.OnBoosterUpdatedCallback(type);
        }
        else
        {
            Debug.LogError($"NOT EXISTED BOOSTER {type}");
        }
    }

    public CBoosterDataCommodity GetBooster(BoosterType type)
    {
        if (this._userBoosterDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryUserBooster == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryUserBooster.TryGetValue(type, out CBoosterDataCommodity booster))
        {
            return booster;
        }
        else
        {
            Debug.LogError($"NOT EXISTED BOOSTER {type}");
            return null;
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
