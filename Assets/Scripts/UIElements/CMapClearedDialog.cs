using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using System;

public class CMapClearedDialog : CBaseDialog
{
    public Image panel_dialog;
    public TextMeshProUGUI tmp_stage_id;
    public Animator[] img_star_get_animators;
    public Image img_collected_dots_bonus_coins_bar;
    public Image img_collected_dots_bonus_shield_bar;
    public Image img_collected_dots_bonus_coins_progress_bar;
    public Image img_collected_dots_bonus_shield_progress_bar;
    public Animator img_bonus_coin_animator;
    public GUI gui_bonus_coin;
    public Button btn_chest_claim;
    public Button btn_next_stage;
    public Button btn_close;

    private int starsCollected = 0;

    private float coins_progress_bar_fill_amount = 0.0f;
    private float shield_progress_bar_fill_amount = 0.0f;

    public override void OnCompleteShow()
    {
        base.OnCompleteShow();

        if (this.data != null)
        {
            if (this.data.GetType() == typeof(CMapResult))
            {
                CMapResult result = data as CMapResult;

                this.starsCollected = result.starsCollected;

                this.coins_progress_bar_fill_amount = result.percentDotsCollected;
                this.shield_progress_bar_fill_amount = result.percentDotsCollected;

                StartCoroutine(coroutinePLayStarCollectedAnim());
            }
            else
            {
                Debug.LogError(this.GetType().Name + " - Wrong data type!");
                return;
            }
        }
        else
        {
            Debug.Log(this.GetType().Name + " - No data found!");
            return;
        }
    }

    public override void OnHide()
    {
        // this._animator.Play(ANIMATOR_HIDE);
    }

    public override void OnCompleteHide()
    {
        Destroy(this.gameObject);
    }

    public void PlayDialogBodyOnShowAnim()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(this.panel_dialog.transform.DOScaleY(1f, 0.3f));
        seq.AppendCallback(this.OnCompleteShow);

        seq.Play();
    }

    private IEnumerator coroutinePLayStarCollectedAnim()
    {
        const float delayTime = 0.5f;

        for (int i = 0; i < this.starsCollected; i++)
        {
            this.img_star_get_animators[i].Play(GameDefine.STAR_GETS_ANIM);

            switch(i)
            {
                case 0:
                    CSoundManager.Instance.PlayFx(GameDefine.STAR_COLLECTED_1_FX_KEY); break;
                case 1:
                    CSoundManager.Instance.PlayFx(GameDefine.STAR_COLLECTED_2_FX_KEY); break;
                case 2:
                    CSoundManager.Instance.PlayFx(GameDefine.STAR_COLLECTED_3_FX_KEY); break;
            }

            yield return new WaitForSeconds(delayTime);
        }

        this.PlayCollectedDotsFillAmount();
    }

    private void PlayCollectedDotsFillAmount()
    {
        
        const float duration = 1.0f;
        this.img_collected_dots_bonus_coins_progress_bar
            .DOFillAmount(this.coins_progress_bar_fill_amount, duration)
            .OnStart(() => CSoundManager.Instance.PlayLoopFx(GameDefine.SCORE_COUNT_FX_KEY))
            .OnComplete(() =>
            {
                CSoundManager.Instance.StopFx();

                if (this.coins_progress_bar_fill_amount == 1.0f)
                {
                    this.PlayBonusAcquiredAnim();
                }
            });
    }

    private void PlayBonusAcquiredAnim()
    {
        CSoundManager.Instance.PlayFx(GameDefine.POWER_UP_OFF_FX_KEY);
        this.img_bonus_coin_animator.Play(GameDefine.NOTIFY_ANIM);
    }

    public void OnBtnClaimChestPressed()
    {
        CSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
        Debug.Log("Show Wheel Bonus");
    }

    public void OnBtnNextStagePressed()
    {
        CSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
        Debug.Log("Load next map");
    }

    public void OnBtnClosePressed()
    {
        Debug.Log("Close dialog and Go back to home scene");
    }
}
