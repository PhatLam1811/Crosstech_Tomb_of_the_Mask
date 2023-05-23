using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameDataManager : MonoSingleton<CGameDataManager>
{
    public CGameDatas _gameData;

    public void OpenApp()
    {
        this.LoadGameData();

        CPlayerBoosterDatas.Instance.AssignCallbackOnBoosterUpdated(this.OnPlayerBoosterUpdated);
    }

    public void LoadGameData()
    {
        try
        {
            if (PlayerPrefs.HasKey(GameDefine.GAME_DATA))
            {
                string jsonData = PlayerPrefs.GetString(GameDefine.GAME_DATA);

                if (!string.IsNullOrEmpty(jsonData))
                {
                    this._gameData = JsonUtility.FromJson<CGameDatas>(jsonData);        
                    Debug.Log(jsonData);
                }
                else
                {
                    Debug.LogError("CAN NOT PARSE USER DATA: " + jsonData);
                    return;
                }
            }
            else
            {
                this.CreateNewGameData();
            }

            this._gameData.OpenApp();
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void SaveGameData()
    {
        string jsonData = JsonUtility.ToJson(this._gameData);
        PlayerPrefs.SetString(GameDefine.GAME_DATA, jsonData);
        Debug.Log("GAME DATA SAVED!");
    }

    public void CreateNewGameData()
    {
        this._gameData = new CGameDatas();
        this._gameData.CreateNew();
        this.SaveGameData();
    }

    public void ClearGameData()
    {
        PlayerPrefs.DeleteKey(GameDefine.GAME_DATA);
        this.CreateNewGameData();
    }

    public void OnPlayerBoosterUpdated(BoosterType type)
    {
        this.SaveGameData();
    }
}
