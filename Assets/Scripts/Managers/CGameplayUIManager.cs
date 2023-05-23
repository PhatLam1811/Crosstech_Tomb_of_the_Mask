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
            CBoosterDataCommodity coinBooster = CPlayerBoosterDatas.Instance.GetBooster(type);
            this._tmpPlayerCoins.text = coinBooster.value.ToString();
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
