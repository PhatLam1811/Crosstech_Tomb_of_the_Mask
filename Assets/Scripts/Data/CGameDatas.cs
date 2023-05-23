using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CGameDatas
{
    public CPlayerBoosterDatas _playerBoosterDatas;

    public void OpenApp()
    {
        this._playerBoosterDatas.OpenApp();
    }

    public void CreateNew()
    {
        this._playerBoosterDatas = new CPlayerBoosterDatas();
        this._playerBoosterDatas.CreateNew();
    }
}
