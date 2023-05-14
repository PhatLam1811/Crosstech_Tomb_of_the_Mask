using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Configs", fileName = "LevelConfigs")]
public class CLevelConfigs : ScriptableObject
{
    #region Singleton
    private static CLevelConfigs _instance;

    public static CLevelConfigs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CGameManager.Instance.GetResourceFile<CLevelConfigs>(GameDefine.LEVEL_CONFIGS_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(CLevelConfigs).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    public List<CLevelConfig> _levelConfigs;
}

[System.Serializable]
public class CLevelConfig
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
