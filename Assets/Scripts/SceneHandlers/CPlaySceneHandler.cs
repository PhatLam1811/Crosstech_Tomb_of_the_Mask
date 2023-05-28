using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlaySceneHandler : MonoSingleton<CPlaySceneHandler>
{
    public bool IsMapBonusCollected(int mapId)
    {
        return CGameDataManager.Instance.GetGameMapData(mapId).isBonusCollected;
    }

    public void ConfirmMapBonusCollected(int mapId)
    {
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.SET_IS_BONUS_COLLECTED, mapId);
    }

    public int GetOnPlayingMapId()
    {
        return CGameplayManager.Instance.GetOnPlayingMapId();
    }

    public void BackToHomeScene()
    {
        CGameManager.Instance.LoadSceneAsync(GameDefine.HOME_SCENE_ID);
    }
}
