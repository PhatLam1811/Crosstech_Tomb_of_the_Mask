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
    public Image img_count_down;
    public TextMeshProUGUI tmp_revive_turn;
    public Animator img_revive_animator;
    public Button btn_purchase;
    public Animator tmp_revive_cost_animator;

    private const int REVIVE_COST = 200;
    private const float REVIVE_DURATION = 5.0f;

    private const string REVIVE_SUCCESS_ANIM = "revived";
    private const string REVIVE_COUNTDOWN_TWEEN = "revive_countdown";

    public override void OnCompleteShow()
    {
        base.OnCompleteShow();
        this.PlayDialogBodyOnShowAnim();
    }

    public override void OnHide()
    {
        this.img_count_down.fillAmount = 1.0f;
        this.img_count_down.gameObject.SetActive(false);
        this.PlayDialogBodyOnCloseAnim(this.OnCompleteHide);
    }

    public override void OnCompleteHide()
    {
        CPlaySceneHandler.Instance.BackToHomeScene();
        Destroy(this.gameObject);
    }

    private void LoadUIComponents()
    {
        this.img_count_down.gameObject.SetActive(true);
        this.img_count_down
            .DOFillAmount(0.0f, REVIVE_DURATION)
            .SetId(this.GetInstanceID() + REVIVE_COUNTDOWN_TWEEN)
            .OnComplete(this.OnHide);
    }

    public void PlayDialogBodyOnShowAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(1f, 0.3f)
            .OnComplete(this.LoadUIComponents);
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
            this.img_count_down.fillAmount = 1.0f;
            this.img_count_down.gameObject.SetActive(false);
            DOTween.Kill(this.GetInstanceID() + REVIVE_COUNTDOWN_TWEEN);
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
        this.img_revive_animator.Play(REVIVE_SUCCESS_ANIM);

        yield return new WaitForSeconds(delay);

        this.PlayDialogBodyOnCloseAnim(() => {
            CGameplayManager.Instance.OnPlayerRevive();
            Destroy(this.gameObject);  
        });
    }
}
