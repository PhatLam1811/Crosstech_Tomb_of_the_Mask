using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using System;

public class CReviveDialog : CBaseDialog
{
    public Image panel_dialog;
    public Image img_count_down;
    public TextMeshProUGUI tmp_revive_turn;
    public Animator img_revive_animator;
    public Button btn_purchase;
    public Button btn_replay;
    public Animator tmp_revive_cost_animator;

    private bool _isRevived;

    private const int REVIVE_COST = 200;
    private const float REVIVE_DURATION = 5.0f;

    private const string REVIVE_SUCCESS_ANIM = "revived";
    private const string REVIVE_FAILED_ANIM = "revive_failed";

    private const string REVIVE_COUNTDOWN_TWEEN = "revive_countdown";

    private void OnDisable()
    {
        DOTween.Kill(this.GetInstanceID() + REVIVE_COUNTDOWN_TWEEN);
    }

    public override void OnShow(object data = null, UnityAction callback = null)
    {
        base.OnShow(data, callback);
        this.ParseData();
    }

    public override void OnCompleteShow()
    {
        base.OnCompleteShow();
        this.PlayDialogBodyOnShowAnim();
    }

    public override void OnHide()
    {
        this.img_count_down.fillAmount = 1.0f;
        this.img_count_down.gameObject.SetActive(false);
        this.PlayDialogBodyOnCloseAnim(false);
    }

    public override void OnCompleteHide()
    {
        CPlaySceneHandler.Instance.BackToHomeScene();
        Destroy(this.gameObject);
    }

    private void ParseData()
    {
        if (this.data != null)
        {
            if (this.data.GetType() == typeof(bool))
            {
                this._isRevived = (bool)this.data;
            }
            else
            {
                Debug.LogError(this.GetType().Name + " - Wrong data type!"); return;
            }
        }
        else
        {
            Debug.LogError(this.GetType().Name + " - No data found!"); return;
        }
    }

    private void LoadUIComponents()
    {
        if (!this._isRevived)
        {
            this.tmp_revive_turn.text = "1";
            this.img_count_down
                .DOFillAmount(0.0f, REVIVE_DURATION)
                .SetId(this.GetInstanceID() + REVIVE_COUNTDOWN_TWEEN)
                .OnComplete(this.OnReviveFailed);
        }
        else
        {
            this.tmp_revive_turn.text = "0";
            this.OnReviveFailed();
        }

        this.btn_replay.gameObject.SetActive(this._isRevived);
        this.btn_purchase.gameObject.SetActive(!this._isRevived);
        this.img_count_down.gameObject.SetActive(!this._isRevived);
    }

    public void PlayDialogBodyOnShowAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(1f, 0.3f)
            .OnComplete(this.LoadUIComponents);
    }

    public void PlayDialogBodyOnCloseAnim(bool isReviveSucces)
    {
        this.panel_dialog.transform
            .DOScaleY(0f, 0.3f)
            .OnComplete(() => {
                if (isReviveSucces)
                {
                    CGameplayManager.Instance.OnPlayerRevive();
                    Destroy(this.gameObject);
                }
                else
                {
                    this.OnCompleteHide();
                }
            });
    }

    public bool GetIsReivived()
    {
        return this._isRevived;
    }

    private void OnRevive()
    {
        this.tmp_revive_turn.text = "0";
        this.img_revive_animator.Play(REVIVE_SUCCESS_ANIM);
        CGameSoundManager.Instance.PlayFx(GameDefine.RESCUE_YES_FX_KEY);
    }

    private void OnReviveFailed()
    {
        this.img_revive_animator.Play(REVIVE_FAILED_ANIM);
        CGameSoundManager.Instance.PlayFx(GameDefine.RESCUE_YES_FX_KEY);
    }

    public void OnBtnPurchaseClicked()
    {
        bool isSuccess = CPlaySceneHandler.Instance.PurchaseRevive(REVIVE_COST);

        if (isSuccess)
        {
            this.img_count_down.fillAmount = 1.0f;
            this.img_count_down.gameObject.SetActive(false);
            DOTween.Kill(this.GetInstanceID() + REVIVE_COUNTDOWN_TWEEN);
            this.OnRevive();
        }
        else
        {
            this.tmp_revive_cost_animator.Play(GameDefine.PURCHASE_FAILED_ANIM);
        }
    }

    public void OnBtnReplayClicked()
    {
        int mapId = CPlaySceneHandler.Instance.GetOnPlayingMapId();
        CPlaySceneHandler.Instance.LoadMap(mapId);
    }
}
