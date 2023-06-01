using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlaySceneHandler : MonoSingleton<CPlaySceneHandler>
{
    public void ShowPauseDialog(Transform canvasPos)
    {
        CGameManager.Instance.ShowDialog<CPauseDialog>(GameDefine.DIALOG_PAUSE_PATH, canvasPos);
    }

    public bool IsLastMap(int mapId)
    {
        return CGameMapDatas.Instance.IsLastMap(mapId);
    }

    public bool IsMapBonusCollected(int mapId)
    {
        return CGameDataManager.Instance.GetGameMapData(mapId).isBonusCollected;
    }

    public bool IsMapChestClaimed(int mapId)
    {
        return CGameDataManager.Instance.GetGameMapData(mapId).isChestClaimed;
    }

    public bool IsMapBonusCoin(int mapId)
    {
        return CMapConfigs.Instance.GetMapConfig(mapId)._bonus == MapBonusType.COIN;
    }

    public void ConfirmBonusCollected(int mapId, BoosterType bonusType, long value)
    {
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.SET_IS_BONUS_COLLECTED, mapId);
        CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.ADD_VALUE, bonusType, value);
    }

    public bool PurchaseRevive(int cost)
    {
        long playerCoin = CGameDataManager.Instance.GetPlayerBoosterData(BoosterType.COIN).value;

        if (playerCoin >= cost)
        {
            CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.SUBTRACT_VALUE, BoosterType.COIN, cost);
            return true;
        }

        return false;
    }

    public int GetOnPlayingMapId()
    {
        return CGameplayManager.Instance.GetOnPlayingMapId();
    }

    public void BackToHomeScene()
    {
        CGameManager.Instance.LoadSceneAsync(GameDefine.HOME_SCENE_ID);
    }

    public long GetPlayerBoosterData(BoosterType boosterType)
    {
        return CGameDataManager.Instance.GetPlayerBoosterData(boosterType).value;
    }

    public bool TryActivatePlayerShield()
    {
        long shieldRemain = CGameDataManager.Instance.GetPlayerBoosterData(BoosterType.SHIELD).value;

        if (shieldRemain > 0)
        {
            CGameplayManager.Instance.OnPlayerShieldStateChanged(true);
            CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.SUBTRACT_VALUE, BoosterType.SHIELD, 1);
            return true;
        }

        return false;
    }

    public void LoadMap(int mapId)
    {
        CGameplayManager.Instance.PlayMap(mapId);
    }
}
