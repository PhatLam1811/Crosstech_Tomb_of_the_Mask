using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMapUpdateType
{
    ADD_GAME_MAP = 0,
    UNLOCK_MAP = 1,
    SET_IS_CLEARED = 2,
    SET_IS_BONUS_COLLECTED = 3,
    SET_IS_CHEST_CLAIMED = 4,
    SET_GAME_MAP_STARS = 5,
}

[System.Serializable]
public class CMapDataCommodity
{
    public int _id;

    public bool isUnlocked;
    public bool isCleared;
    public bool isBonusCollected;
    public bool isChestClaimed;

    public int stars;

    private CMapDataCommodity() { }

    public CMapDataCommodity(int id)
    {
        this._id = id;

        this.isUnlocked = false;
        this.isCleared = false;
        this.isBonusCollected = false;
        this.isChestClaimed = false;

        this.stars = 0;
    }

    public void SetIsClearedTrue()
    {
        this.isCleared = true;
    }

    public void UnlockMap()
    {
        this.isUnlocked = true;
    }

    public void SetIsBonusCollectedTrue()
    {
        this.isBonusCollected = true;
    }

    public void SetIsChestClaimedTrue()
    {
        this.isChestClaimed = true;
    }

    public void SetGameMapStars(int stars)
    {
        this.stars = stars;
    }
}

[System.Serializable]
public class CGameMapDatas
{
    public static CGameMapDatas Instance => CGameDataManager.Instance._gameData?._gameMapDatas;

    public List<CMapDataCommodity> _mapDatas;
    public Dictionary<int, CMapDataCommodity> _dictionaryMapDatas;

    private int _lastUnlockedMapId;
    private int _lastMapId;

    public void OpenApp()
    {
        this.SetUpDictionary();
    }

    private void SetUpDictionary()  
    {
        this._dictionaryMapDatas = new Dictionary<int, CMapDataCommodity>();
        this._lastUnlockedMapId = 1;

        foreach (CMapDataCommodity mapData in this._mapDatas)
        {
            if (!this._dictionaryMapDatas.ContainsKey(mapData._id))
            {
                this._dictionaryMapDatas.Add(mapData._id, mapData);
                
                if (mapData.isUnlocked && mapData._id > this._lastUnlockedMapId)
                {
                    this._lastUnlockedMapId = mapData._id;
                }
            }
        }

        int lastMapIndex = this._dictionaryMapDatas.Count - 1;
        this._lastMapId = this._mapDatas[lastMapIndex]._id;
    }

    public void CreateNew()
    {
        this._mapDatas = new List<CMapDataCommodity>();

        this.LoadMapConfigs();

        this._mapDatas[0].isUnlocked = true;
    }

    private void LoadMapConfigs()
    {
        foreach (CMapConfig config in CMapConfigs.Instance._mapConfigs)
        {
            CMapDataCommodity newMap = new CMapDataCommodity(id: config._id);
            this._mapDatas.Add(newMap);
        }
    }

    public void AddNewGameMap(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (!this._dictionaryMapDatas.ContainsKey(id))
        {
            CMapDataCommodity newBooster = new CMapDataCommodity(id);
            this._dictionaryMapDatas.Add(id, newBooster);
        }
        else
        {
            Debug.Log($"EXISTED MAP {id}");
        }
    }

    public void UnlockGameMap(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapDataCommodity map))
        {
            if (!map.isUnlocked)
            {
                this._lastUnlockedMapId = id;
                map.UnlockMap();
            }
        }
        else
        {
            Debug.Log($"NOT EXISTED MAP {id}");
        }
    }

    public void SetGameMapCleared(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapDataCommodity map))
        {
            map.SetIsClearedTrue();
        }
        else
        {
            Debug.Log($"NOT EXISTED MAP {id}");
        }
    }

    public void SetGameMapBonusCollected(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapDataCommodity map))
        {
            map.SetIsBonusCollectedTrue();
        }
        else
        {
            Debug.Log($"NOT EXISTED MAP {id}");
        }
    }

    public void SetGameMapChestClaimed(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapDataCommodity map))
        {
            map.SetIsChestClaimedTrue();
        }
        else
        {
            Debug.Log($"NOT EXISTED MAP {id}");
        }
    }

    public void SetGameMapStars(int id, int stars)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapDataCommodity map))
        {
            if (stars > map.stars)
            {
                map.SetGameMapStars(stars);
            }
        }
        else
        {
            Debug.Log($"NOT EXISTED MAP {id}");
        }
    }

    public CMapDataCommodity GetGameMapData(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapDataCommodity map))
        {
            return map;
        }
        else
        {
            Debug.Log($"NOT EXISTED MAP {id}");
            return null;
        }
    }

    public int GetLastUnlockedMap()
    {
        return this._lastUnlockedMapId;
    }

    public int GetMapsCount()
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        return this._dictionaryMapDatas.Count;
    }

    public bool IsLastMap(int id)
    {
        return id == this._lastMapId;
    }
}
