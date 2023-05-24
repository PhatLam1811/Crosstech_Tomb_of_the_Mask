using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CGameDatas
{
    public static CGameDatas Instance => CGameDataManager.Instance._gameData;

    public CPlayerBoosterDatas _playerBoosterDatas;
    public CMapDatas _mapDatas;

    public void OpenApp()
    {
        this._playerBoosterDatas.OpenApp();
    }

    public void CreateNew()
    {
        this._playerBoosterDatas = new CPlayerBoosterDatas();
        this._mapDatas = new CMapDatas();

        this._playerBoosterDatas.CreateNew();
        this._mapDatas.CreateNew();
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
    }
}
