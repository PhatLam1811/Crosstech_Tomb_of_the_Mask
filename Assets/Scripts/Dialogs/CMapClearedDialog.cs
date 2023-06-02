using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using System;

public class CMapClearedDialogData
{
    public float percentDotsCollected;
    public int starsCollected;
}

public class CMapClearedDialog : CBaseDialog
{
    public Image panel_dialog;
    public TextMeshProUGUI tmp_stage_id;
    public Animator[] img_star_get_animators;
    public Image img_bonus_coin_bar;
    public Image img_bonus_shield_bar;
    public Image img_bonus_coin_progress_bar;
    public Image img_bonus_shield_progress_bar;
    public Animator img_bonus_coin_animator;
    public GameObject gui_bonus_coin;
    public Button btn_chest_claim;
    public Image img_chest_unavailable;
    public Button btn_next_stage;
    public TextMeshProUGUI tmp_next_stage;
    public Button btn_close;
    public GameObject chest_bonus;

    private bool _isBonusCoin;

    private int starsCollected = 0;
    private float percentDotsCollected = 0.0f;

    private const float COMING_SOON_FONT_SIZE = 30.0f;

    public override void OnShow(object data = null, UnityAction callback = null)
    {
        base.OnShow(data, callback);
        this.ParseData();
        this.LoadUIComponents();
    }

    public override void OnCompleteShow()
    {
        base.OnCompleteShow();
        this.PlayDialogBodyOnShowAnim();
    }

    public override void OnHide()
    {
        this.PlayDialogBodyOnCloseAnim();
    }

    public override void OnCompleteHide()
    {
        CPlaySceneHandler.Instance.BackToHomeScene();
        Destroy(this.gameObject);
    }

    private void LoadUIComponents()
    {
        int mapId = CPlaySceneHandler.Instance.GetOnPlayingMapId();

        this.tmp_stage_id.text = "STAGE " + mapId.ToString(); 

        bool isChestClaimed = CPlaySceneHandler.Instance.IsMapChestClaimed(mapId);
        this.btn_chest_claim.gameObject.SetActive(!isChestClaimed);
        this.img_chest_unavailable.gameObject.SetActive(isChestClaimed);

        this._isBonusCoin = CPlaySceneHandler.Instance.IsMapBonusCoin(mapId);
        this.img_bonus_coin_bar.gameObject.SetActive(this._isBonusCoin);
        this.img_bonus_shield_bar.gameObject.SetActive(!this._isBonusCoin);

        bool isLastMap = CPlaySceneHandler.Instance.IsLastMap(mapId);
        if (isLastMap)
        {
            this.btn_next_stage.onClick.RemoveListener(this.OnBtnNextStagePressed);
            this.btn_next_stage.onClick.AddListener(this.OnBtnCloseClicked);
            this.tmp_next_stage.fontSize = COMING_SOON_FONT_SIZE;
            this.tmp_next_stage.text = "COMING SOON";
        }
        else
        {
            this.btn_next_stage.onClick.AddListener(this.OnBtnNextStagePressed);
        }
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
            .OnComplete(() => {
                StartCoroutine(coroutinePLayStarCollectedAnim());
            });
    }

    private void PlayDialogBodyOnCloseAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(0f, 0.3f)
            .OnComplete(this.OnCompleteHide);
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

        if (this._isBonusCoin)
        {
            this.img_bonus_coin_progress_bar
                .DOFillAmount(this.percentDotsCollected, duration)
                .OnStart(() => CGameSoundManager.Instance.PlayLoopFx(GameDefine.DOTS_COUNT_FX_KEY))
                .OnComplete(() =>
                {
                    CGameSoundManager.Instance.StopFx();
                    this.ClaimMapBonus();
                    this.btn_close.gameObject.SetActive(true);
                    this.btn_next_stage.gameObject.SetActive(true);
                });
        }
        else
        {
            this.img_bonus_shield_progress_bar
                .DOFillAmount(this.percentDotsCollected, duration)
                .OnStart(() => CGameSoundManager.Instance.PlayLoopFx(GameDefine.DOTS_COUNT_FX_KEY))
                .OnComplete(() =>
                {
                    CGameSoundManager.Instance.StopFx();
                    this.ClaimMapBonus();
                    this.btn_close.gameObject.SetActive(true);
                    this.btn_next_stage.gameObject.SetActive(true);
                });
        }
    }

    private void ClaimMapBonus()
    {
        int mapId = CPlaySceneHandler.Instance.GetOnPlayingMapId();

        if (this.percentDotsCollected == 1.0f
         && !CPlaySceneHandler.Instance.IsMapBonusCollected(mapId))
        {
            if (this._isBonusCoin)
            {
                StartCoroutine(this.PlayCoinBonusClaimAnim());
                CPlaySceneHandler.Instance.ConfirmBonusCollected(mapId, BoosterType.COIN, 25);
            }
            else
            {
                CPlaySceneHandler.Instance.ConfirmBonusCollected(mapId, BoosterType.SHIELD, 1);
            }
        }
    }

    public void OnCoinBonusClaimedComplete()
    {
        this.img_bonus_coin_bar.gameObject.SetActive(false);
        this.gui_bonus_coin.SetActive(true);
    }

    private IEnumerator PlayCoinBonusClaimAnim()
    {
        float delay = 1.0f;

        CGameSoundManager.Instance.PlayFx(GameDefine.BONUS_CLAIMED_FX_KEY);
        this.img_bonus_coin_animator.Play(GameDefine.NOTIFY_ANIM);

        yield return new WaitForSeconds(delay);

        this.OnCoinBonusClaimedComplete();
    }

    public void OnBtnClaimChestPressed()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
        CPlaySceneHandler.Instance.PseudoClaimFirstClearChest();
        this.btn_chest_claim.gameObject.SetActive(false);
        this.chest_bonus.SetActive(true);
    }

    public void OnBtnNextStagePressed()
    {
        int nextMapId = CPlaySceneHandler.Instance.GetOnPlayingMapId() + 1;
        CPlaySceneHandler.Instance.LoadMap(nextMapId);
    }
}
