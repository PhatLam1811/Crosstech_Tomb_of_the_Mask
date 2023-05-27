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

    public bool IsLastUnlockedMap(int mapId)
    {
        return mapId == CGameDataManager.Instance._gameData._gameMapDatas.GetCurrentUnlockedMapId();
    }

    public bool IsMapUnlocked(int mapId)
    {
        return CGameDataManager.Instance.GetGameMapData(mapId).isUnlocked;
    }

    public void PlayMap(int mapId)
    {
        CGameManager.Instance.LoadScene(GameDefine.PLAY_SCENE_ID);
        CGameplayManager.Instance.PlayMap(mapId);
    }
}
