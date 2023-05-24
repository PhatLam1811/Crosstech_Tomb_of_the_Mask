using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CMapData
{
    public int _id;
    public int stars;

    private CMapData() { }

    public CMapData(int id, int stars)
    {
        this._id = id;
        this.stars = stars;
    }

    public void SetStars(int stars)
    {
        this.stars = stars;
    }
}

[System.Serializable]
public class CMapDatas
{
    public static CMapDatas Instance => CGameDataManager.Instance._gameData?._mapDatas;

    public List<CMapData> _mapDatas;
    public Dictionary<int, CMapData> _dictionaryMapDatas;

    private const int INITIAL_STARS = 0;

    public void OpenApp()
    {
        this.SetUpDictionary();
    }

    private void SetUpDictionary()
    {
        this._dictionaryMapDatas = new Dictionary<int, CMapData>();

        foreach (CMapData mapData in this._mapDatas)
        {
            if (!this._dictionaryMapDatas.ContainsKey(mapData._id))
            {
                this._dictionaryMapDatas.Add(mapData._id, mapData);
            }
        }
    }

    public void CreateNew()
    {
        this._mapDatas = new List<CMapData>();
        
        for (int i = 1; i <= 10; i++)
        {
            this._mapDatas.Add(new CMapData(id: i, stars: INITIAL_STARS));
        }
    }

    public void AddNewMap(int id, int stars)
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
            CMapData newBooster = new CMapData(id, stars);
            this._dictionaryMapDatas.Add(id, newBooster);
        }
        else
        {
            Debug.LogError($"EXISTED MAP {id}");
        }
    }

    public void SetMapStars(int id, int stars)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapData map))
        {
            map.SetStars(stars);
        }
        else
        {
            Debug.LogError($"NOT EXISTED MAP {id}");
        }
    }

    public long GetMapStars(int id)
    {
        if (this._mapDatas == null)
        {
            this.CreateNew();
        }

        if (this._dictionaryMapDatas == null)
        {
            this.SetUpDictionary();
        }

        if (this._dictionaryMapDatas.TryGetValue(id, out CMapData map))
        {
            return map.stars;
        }
        else
        {
            Debug.LogError($"NOT EXISTED MAP {id}");
            return -1;
        }
    }
}
