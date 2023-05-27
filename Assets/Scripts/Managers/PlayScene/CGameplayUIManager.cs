using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CGameplayUIManager : MonoSingleton<CGameplayUIManager>
{
    public Transform _canvasTransform; 

    public TextMeshProUGUI _tmpPlayerCoins;
    public CGameplayStarsHolder _starsHolder;
    public Button _btnPause;
    public Button _btnShield;

    public void StartGame()
    {
        CPlayerBoosterDatas.Instance.AssignCallbackOnBoosterUpdated(this.OnPlayerBoosterUpdated);
    }

    public Transform GetCanvasPos()
    {
        return this._canvasTransform;
    }

    public void OnPlayerBoosterUpdated(BoosterType type)
    {
        if (type == BoosterType.COIN)
        {
            long playerCoins = CGameDataManager.Instance.GetPlayerBoosterData(type).value;
            
            if (playerCoins != -1)
            {
                this._tmpPlayerCoins.text = playerCoins.ToString();
            }
        }
    }

    public void OnPlayerCollectedStar(int starNumber) 
    {
        this._starsHolder.OnStarCollected(starNumber);
    }

    public void OnPauseBtnClicked()
    {
        Debug.Log("On Paused!");
    }

    public void OnShieldBtnClicked()
    {
        Debug.Log("Shield On!");
    }
}
