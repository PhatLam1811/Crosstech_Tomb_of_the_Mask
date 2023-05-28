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
    private float percentDotsCollected = 0.0f;

    private const int TWEEN_DIALOG_SHOW_ID = 0;
    private const int TWEEN_DIALOG_CLOSE_ID = 1;

    public override void OnCompleteShow()
    {
        base.OnCompleteShow();
        DOTween.Kill(TWEEN_DIALOG_SHOW_ID);
        this.ParseData();
        StartCoroutine(coroutinePLayStarCollectedAnim());
        Debug.Log("On Complete Show");
    }

    public override void OnHide()
    {
        this.PlayDialogBodyOnCloseAnim();
    }

    public override void OnCompleteHide()
    {
        DOTween.Kill(TWEEN_DIALOG_CLOSE_ID);
        CPlaySceneHandler.Instance.BackToHomeScene();
        Destroy(this.gameObject);
    }

    private void ParseData()
    {
        if (this.data != null)
        {
            if (this.data.GetType() == typeof(CMapClearedDialogData))
            {
                CMapClearedDialogData result = data as CMapClearedDialogData;
                this.starsCollected = result.starsCollected;
                this.percentDotsCollected = result.percentDotsCollected;
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

    public void PlayDialogBodyOnShowAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(1f, 0.3f)
            .OnComplete(this.OnCompleteShow)
            .SetId(TWEEN_DIALOG_SHOW_ID);
    }

    private void PlayDialogBodyOnCloseAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(0f, 0.2f)
            .OnComplete(this.OnCompleteHide)
            .SetId(TWEEN_DIALOG_CLOSE_ID);
    }

    private IEnumerator coroutinePLayStarCollectedAnim()
    {
        const float delayTime = 0.3f;

        for (int i = 0; i < this.starsCollected; i++)
        {
            this.img_star_get_animators[i].Play(GameDefine.STAR_GETS_ANIM);

            switch(i)
            {
                case 0:
                    CGameSoundManager.Instance.PlayFx(GameDefine.STAR_COLLECTED_1_FX_KEY); break;
                case 1:
                    CGameSoundManager.Instance.PlayFx(GameDefine.STAR_COLLECTED_2_FX_KEY); break;
                case 2:
                    CGameSoundManager.Instance.PlayFx(GameDefine.STAR_COLLECTED_3_FX_KEY); break;
            }

            yield return new WaitForSeconds(delayTime);
        }

        this.PlayCollectedDotsFillAmount();
    }

    private void PlayCollectedDotsFillAmount()
    {
        const float duration = 1.0f;
        this.img_collected_dots_bonus_coins_progress_bar
            .DOFillAmount(this.percentDotsCollected, duration)
            .OnStart(() => CGameSoundManager.Instance.PlayLoopFx(GameDefine.DOTS_COUNT_FX_KEY))
            .OnComplete(() =>
            {
                CGameSoundManager.Instance.StopFx();
                this.btn_close.gameObject.SetActive(true);
                this.btn_next_stage.gameObject.SetActive(true);
                this.ClaimMapBonus();
            });
    }

    private void ClaimMapBonus()
    {
        int mapId = CPlaySceneHandler.Instance.GetOnPlayingMapId();

        if (this.percentDotsCollected == 1.0f
         && !CPlaySceneHandler.Instance.IsMapBonusCollected(mapId))
        {
            this.PlayBonusAcquiredAnim();
            CPlaySceneHandler.Instance.ConfirmMapBonusCollected(mapId);
        }

        this.btn_close.gameObject.SetActive(true);
        this.btn_next_stage.gameObject.SetActive(true);
    }

    private void PlayBonusAcquiredAnim()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.BONUS_CLAIMED_FX_KEY);
        this.img_bonus_coin_animator.Play(GameDefine.NOTIFY_ANIM);
    }

    public void OnBtnClaimChestPressed()
    {
        Debug.Log("Show Wheel Bonus");
    }

    public void OnBtnNextStagePressed()
    {
        int currentMapId = CPlaySceneHandler.Instance.GetOnPlayingMapId();
        CGameplayManager.Instance.PlayMap(currentMapId + 1);
    }

    public void OnBtnClosePressed()
    {
        this.OnHide();
    }
}
