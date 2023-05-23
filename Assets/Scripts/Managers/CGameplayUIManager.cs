using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CGameplayUIManager : MonoSingleton<CGameplayUIManager>
{
    public TextMeshProUGUI _tmpPlayerCoins;
    public Button _btnPause;
    public Button _btnShield;

    public void StartGame()
    {
        CPlayerBoosterDatas.Instance.AssignCallbackOnBoosterUpdated(this.OnPlayerBoosterUpdated);
    }

    public void OnPlayerBoosterUpdated(BoosterType type)
    {
        if (type == BoosterType.COIN)
        {
            long playerCoins = CPlayerBoosterDatas.Instance.GetBoosterValue(type);
            
            if (playerCoins != -1)
            {
                this._tmpPlayerCoins.text = playerCoins.ToString();
            }
        }
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
