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

    public Vector3Int _playerStartPosition;
    public Vector3Int _exitPosition;

    public MapBonusType _bonus;

    public List<CCollectableObjectPositionConfig> _collectableObjectConfigs;
    public List<CTrapObjectConfig> _trapObjectConfigs;
}

[System.Serializable]
public class CCollectableObjectPositionConfig
{
    public MapCollectableType _id;
    public Vector3Int position;
}

[System.Serializable]
public class CTrapObjectConfig
{
    public MapTrapType _id;
    public Vector3Int position;
}

[System.Serializable]
public enum MapBonusType
{
    NONE = -1,
    COIN = 0,
    SHIELD = 1
}

[System.Serializable]
public enum MapCollectableType
{
    GAME_DOTS = 0,
    COIN = 1,
    STAR = 2
}

[System.Serializable]
public enum MapTrapType
{
    NONE = -1,
   
    CANNON_T = 0,
    CANNON_L = 1,
    CANNON_D = 2,
    CANNON_R = 3,

    BAT = 4,
}
