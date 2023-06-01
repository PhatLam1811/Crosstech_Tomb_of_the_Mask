using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CPauseDialog : CBaseDialog
{
    public Image panel_dialog;

    public TextMeshProUGUI tmp_stage_id;

    public Button btn_bgm_setting;
    public Button btn_fx_setting;
    public Button btn_quit;
    public Button btn_replay;

    public Image img_bgm_setting_icon;
    public Image img_fx_setting_icon;

    public Sprite sprite_bgm_muted_icon;
    public Sprite sprite_bgm_unmuted_icon;

    public Sprite sprite_fx_muted_icon;
    public Sprite sprite_fx_unmuted_icon;

    private bool _isQuit = false;
    private bool _isReplay = false;

    private const string SHOW_PANEL_TWEEN = "show_panel";

    private void OnDisable()
    {
        DOTween.Kill(this.GetInstanceID() + SHOW_PANEL_TWEEN);  
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
        base.OnCompleteHide();
        Destroy(this.gameObject);
    }

    public void PlayDialogBodyOnShowAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(1f, 0.3f)
            .SetId(this.GetInstanceID() + SHOW_PANEL_TWEEN);
    }

    public void PlayDialogBodyOnCloseAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(0f, 0.3f)
            .OnComplete(this.OnBodyCloseAnimComplete);
    }

    public void OnBodyCloseAnimComplete()
    {
        if (this._isQuit)
        {
            CPlaySceneHandler.Instance.BackToHomeScene();
        }

        if (this._isReplay)
        {
            int currentMapId = CPlaySceneHandler.Instance.GetOnPlayingMapId();
            CPlaySceneHandler.Instance.LoadMap(currentMapId);
        }

        if (!this._isQuit && !this._isReplay)
        {
            CGameplayManager.Instance.OnPlayerResume();
        }

        this.OnCompleteHide();
    }

    public void OnQuitBtnClicked()
    {
        this._isQuit = true;
        this.OnHide();
    }

    public void OnReplayBtnClicked()
    {
        this._isReplay = true;
        this.OnHide();
    }
}
