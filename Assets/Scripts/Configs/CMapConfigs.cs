using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Map Configs", fileName = "MapConfigs")]
public class CMapConfigs : ScriptableObject
{
    #region Singleton
    private static CMapConfigs _instance;

    public static CMapConfigs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CGameManager.Instance.GetResourceFile<CMapConfigs>(GameDefine.MAP_CONFIGS_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(CMapConfigs).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    public List<CMapConfig> _mapConfigs;
}

[System.Serializable]
public class CMapConfig
{
    public int _id;

    public List<CCollectableObjectPositionConfig> _collectableObjectPositionConfigs;
}

[System.Serializable]
public class CCollectableObjectPositionConfig
{
    public int _id;

    public int x;
    public int y;
}
