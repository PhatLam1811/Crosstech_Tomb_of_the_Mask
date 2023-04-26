using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Configs", fileName = "PlayerConfigs")]
public class CPlayerConfigs : ScriptableObject
{
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
