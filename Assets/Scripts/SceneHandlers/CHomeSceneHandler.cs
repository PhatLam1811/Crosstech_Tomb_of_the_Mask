using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHomeSceneHandler : MonoSingleton<CHomeSceneHandler>
{
    public int GetPlayerLevel()
    {
        return CGameDataManager.Instance.GetPlayerLevelData().level;
    }

    public int GetPlayerRequiredEXPToNextLevel()
    {
        return CGameDataManager.Instance.GetPlayerLevelData().requiredExpToNextLevel;
    }

    public int GetPlayerEXP()
    {
        return CGameDataManager.Instance.GetPlayerLevelData().exp;
    }

    public long GetPlayerCoins()
    {
        return CGameDataManager.Instance.GetPlayerBoosterData(BoosterType.COIN).value;
    }

    public long GetPlayerEnergy()
    {
        return CGameDataManager.Instance.GetPlayerBoosterData(BoosterType.ENERGY).value;
    }

    public int GetLastUnlockedMap()
    {
        return CGameDataManager.Instance._gameData._gameMapDatas.GetLastUnlockedMap();
    }

    public long GetMapStars(int mapId)
    {
        return CGameDataManager.Instance.GetGameMapData(mapId).stars;
    }

    public bool IsMapUnlocked(int mapId)
    {
        CMapDataCommodity mapData = CGameDataManager.Instance.GetGameMapData(mapId);
        
        if (mapData == null || !mapData.isUnlocked)
        {
            return false;
        }

        return true;
    }

    public bool IsLastMap(int mapId)
    {
        return CGameMapDatas.Instance.IsLastMap(mapId);
    }

    public int GetMapsCount()
    {
        return CGameMapDatas.Instance.GetMapsCount();
    }

    public void PlayMap(int mapId)
    {
        CGameManager.Instance.LoadScene(GameDefine.PLAY_SCENE_ID);
        CGameplayManager.Instance.PlayMap(mapId);
    }
}
