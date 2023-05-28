using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CGameDatas
{
    public static CGameDatas Instance => CGameDataManager.Instance._gameData;

    public CPlayerBoosterDatas _playerBoosterDatas;
    public CPlayerLevelData _playerLevelData;
    public CGameMapDatas _gameMapDatas;

    public void OpenApp()
    {
        this._playerBoosterDatas.OpenApp();
    }

    public void CreateNew()
    {
        this._playerBoosterDatas = new CPlayerBoosterDatas();
        this._playerLevelData = new CPlayerLevelData();
        this._gameMapDatas = new CGameMapDatas();

        this._playerBoosterDatas.CreateNew();
        this._playerLevelData.CreateNew();
        this._gameMapDatas.CreateNew();
    }

    public void UpdatePlayerBooster(BoosterUpdateType updateType, BoosterType boosterType, long value)
    {
        if (this._playerBoosterDatas == null)
        {
            Debug.LogError($"DATA NULL OF TYPE: {typeof(CPlayerBoosterDatas)}!"); return;
        }

        switch(updateType)
        {
            case BoosterUpdateType.ADD_VALUE:
                this._playerBoosterDatas.AddValueBooster(boosterType, value); break;
            case BoosterUpdateType.SUBTRACT_VALUE:
                this._playerBoosterDatas.SubtractValueBooster(boosterType, value); break;
            case BoosterUpdateType.SET_VALUE:
                this._playerBoosterDatas.SetValueBooster(boosterType, value); break;
        }

        CGameDataManager.Instance.SaveGameData();
    }

    public void UpdatePlayerLevel(int exp)
    {
        if (this._playerLevelData == null)
        {
            Debug.LogError($"DATA NULL OF TYPE: {typeof(CPlayerLevelData)}!"); return;
        }

        this._playerLevelData.AddEXPValue(exp);

        CGameDataManager.Instance.SaveGameData();
    }

    public void UpdateGameMap(GameMapUpdateType updateType, int id, int stars)
    {
        if (this._gameMapDatas == null)
        {
            Debug.LogError($"DATA NULL OF TYPE: {typeof(CMapDataCommodity)}!"); return;
        }

        switch (updateType)
        {
            case GameMapUpdateType.ADD_GAME_MAP:
                this._gameMapDatas.AddNewGameMap(id); break;
            case GameMapUpdateType.UNLOCK_MAP:
                this._gameMapDatas.UnlockGameMap(id); break;
            case GameMapUpdateType.SET_IS_CLEARED:
                this._gameMapDatas.SetGameMapCleared(id); break;
            case GameMapUpdateType.SET_IS_BONUS_COLLECTED:
                this._gameMapDatas.SetGameMapBonusCollected(id); break;
            case GameMapUpdateType.SET_IS_CHEST_CLAIMED:
                this._gameMapDatas.SetGameMapChestClaimed(id); break;
            case GameMapUpdateType.SET_GAME_MAP_STARS:
                this._gameMapDatas.SetGameMapStars(id, stars); break;
        }

        CGameDataManager.Instance.SaveGameData();
    }
}
