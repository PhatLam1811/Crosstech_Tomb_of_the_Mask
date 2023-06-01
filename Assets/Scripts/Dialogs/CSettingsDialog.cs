using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class CSettingsDialog : CBaseDialog
{
    public Image panel_dialog;

    public Button btn_bgm_setting;
    public Button btn_fx_setting;

    public Image img_bgm_setting_icon;
    public Image img_fx_setting_icon;

    public Sprite sprite_bgm_muted_icon;
    public Sprite sprite_bgm_unmuted_icon;

    public Sprite sprite_fx_muted_icon;
    public Sprite sprite_fx_unmuted_icon;

    private const string SHOW_PANEL_TWEEN = "show_panel";

    private void OnDisable()
    {
        DOTween.Kill(this.GetInstanceID() + SHOW_PANEL_TWEEN);
    }

    public override void OnShow(object data = null, UnityAction callback = null)
    {
        base.OnShow(data, callback);
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
        base.OnCompleteHide();
        Destroy(this.gameObject);
    }

    private void LoadUIComponents()
    {
        this.LoadBtnBGMSettingIcon();
        this.LoadBtnFxSettingIcon();
    }

    private void LoadBtnBGMSettingIcon()
    {
        bool isBGMOn = CHomeSceneHandler.Instance.GetBGMSettings();
        if (isBGMOn)
            this.img_bgm_setting_icon.sprite = sprite_bgm_unmuted_icon;
        else
            this.img_bgm_setting_icon.sprite = sprite_bgm_muted_icon;
    }

    private void LoadBtnFxSettingIcon()
    {
        bool isFxOn = CHomeSceneHandler.Instance.GetFxSettings();
        if (isFxOn)
            this.img_fx_setting_icon.sprite = sprite_fx_unmuted_icon;
        else
            this.img_fx_setting_icon.sprite = sprite_fx_muted_icon;
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
            .OnComplete(this.OnCompleteHide);
    }

    public void OnBtnSettingBGMClicked()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
        CHomeSceneHandler.Instance.ChangeBGMSettings();

        this.LoadBtnBGMSettingIcon();
    }

    public void OnBtnSettingFxClicked()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
        CHomeSceneHandler.Instance.ChangeFxSettings();

        this.LoadBtnFxSettingIcon();
    }
}
