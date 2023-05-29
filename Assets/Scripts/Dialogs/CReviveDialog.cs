using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class CReviveDialog : CBaseDialog
{
    public Image panel_dialog;
    public TextMeshProUGUI tmp_revive_turn;
    public Animator img_revive_animator;
    public Button btn_purchase;
    public Animator tmp_revive_cost_animator;

    private const int REVIVE_COST = 200;

    private const string REVIVE_SUCCESS = "revived";

    public override void OnHide()
    {
        this.PlayDialogBodyOnCloseAnim(this.OnCompleteHide);
    }

    public override void OnCompleteHide()
    {
        CPlaySceneHandler.Instance.BackToHomeScene();
        Destroy(this.gameObject);
    }

    public void PlayDialogBodyOnShowAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(1f, 0.3f)
            .OnComplete(this.OnCompleteShow);
    }

    private void PlayDialogBodyOnCloseAnim(TweenCallback callback)
    {
        this.panel_dialog.transform
            .DOScaleY(0f, 0.3f)
            .OnComplete(callback);
    }

    public void OnBtnCloseClicked()
    {
        this.OnHide();
    }

    public void OnBtnPurchaseClicked()
    {
        bool isSuccess = CPlaySceneHandler.Instance.PurchaseRevive(REVIVE_COST);

        if (isSuccess)
        {
            StartCoroutine(this.OnPlayerReviveSuccess());
        }
        else
        {
            this.tmp_revive_cost_animator.Play(GameDefine.PURCHASE_FAILED_ANIM);
        }
    }

    private IEnumerator OnPlayerReviveSuccess()
    {
        float delay = 1.0f;

        this.tmp_revive_turn.text = "0";
        this.img_revive_animator.Play(REVIVE_SUCCESS);

        yield return new WaitForSeconds(delay);

        this.PlayDialogBodyOnCloseAnim(() => {
            CGameplayManager.Instance.OnPlayerRevive();
            Destroy(this.gameObject);  
        });
    }
}
