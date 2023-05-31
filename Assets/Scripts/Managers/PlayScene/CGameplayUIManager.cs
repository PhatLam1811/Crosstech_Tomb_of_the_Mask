using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class CGameplayUIManager : MonoSingleton<CGameplayUIManager>
{
    public Transform _canvasTransform; 

    public TextMeshProUGUI _tmpPlayerCoins;
    public CGameplayStarsHolder _starsHolder;
    public Button _btnPause;
    public Button _btnShield;
    public TextMeshProUGUI _tmpShieldNumber;
    public Image _imgShieldRemainingBar;
    public Image _imgShieldRemainingProgress;

    private const string TWEEN_SHIELD_PROGRESS = "TWEEN_SHIELD_PROGRESS";

    public void StartGame()
    {
        CPlayerBoosterDatas.Instance.AssignCallbackOnBoosterUpdated(this.OnPlayerBoosterUpdated);
        this.LoadUIComponents();
    }

    private void LoadUIComponents()
    {
        this._tmpPlayerCoins.text = CPlaySceneHandler.Instance.GetPlayerBoosterData(BoosterType.COIN).ToString();
        this._tmpShieldNumber.text = CPlaySceneHandler.Instance.GetPlayerBoosterData(BoosterType.SHIELD).ToString();
    }

    public Transform GetCanvasPos()
    {
        return this._canvasTransform;
    }

    public void OnPlayerBoosterUpdated(BoosterType type)
    {
        switch(type)
        {
            case BoosterType.COIN:
                long playerCoins = CPlaySceneHandler.Instance.GetPlayerBoosterData(type);
                if (playerCoins != -1)
                {
                    this._tmpPlayerCoins.text = playerCoins.ToString();
                }
                break;
            case BoosterType.SHIELD:
                long playerShields = CPlaySceneHandler.Instance.GetPlayerBoosterData(type);
                this._tmpShieldNumber.text = playerShields.ToString();
                break;
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
        this.OnPlayerShieldUp();
    }

    public void OnPlayerShieldUp()
    {
        if (!CPlaySceneHandler.Instance.TryActivatePlayerShield()) return;

        this._imgShieldRemainingBar.gameObject.SetActive(true);
        this._imgShieldRemainingProgress.fillAmount = 1.0f;

        float shieldDuration = 30.0f;
        this._imgShieldRemainingProgress
            .DOFillAmount(0.0f, shieldDuration)
            .SetId(this.GetInstanceID() + TWEEN_SHIELD_PROGRESS)
            .OnComplete(() => {
                this.OnPlayerShieldDown();
                CGameplayManager.Instance.OnPlayerShieldExpired();
            });
    }

    public void OnPlayerShieldDown()
    {
        DOTween.Kill(this.GetInstanceID() + TWEEN_SHIELD_PROGRESS);
        this._imgShieldRemainingBar.gameObject.SetActive(false);
    }
}
