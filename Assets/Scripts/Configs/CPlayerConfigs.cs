using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Configs", fileName = "PlayerConfigs")]
public class CPlayerConfigs : ScriptableObject
{
    #region Singleton
    private static CPlayerConfigs _instance;

    public static CPlayerConfigs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CGameManager.Instance.GetResourceFile<CPlayerConfigs>(GameDefine.PLAYER_CONFIGS_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(CPlayerConfigs).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    public int maxLife;
    public float speed;
    public int defaultMask;
    public List<CMaskConfig> masks;
}

[System.Serializable]
public class CMaskConfig
{
    public int _id;
    public int price;
    public List<CMaskBoostConfig> boostConfigs;
}

[System.Serializable]
public class CMaskBoostConfig
{
    public int _id;
    public float value;
}
