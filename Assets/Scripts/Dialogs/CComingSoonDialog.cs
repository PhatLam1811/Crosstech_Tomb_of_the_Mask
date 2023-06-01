using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class CComingSoonDialog : CBaseDialog
{
    public Image panel_dialog;
    public TextMeshProUGUI tmp_feature;

    private string _comingSoonFeature;

    private void OnDisable()
    {
        Debug.Log(this.panel_dialog.DOKill());
    }

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
        base.OnCompleteHide();
        Destroy(this.gameObject);
    }

    private void ParseData()
    {
        if (this.data != null)
        {
            if (this.data.GetType() == typeof(string))
            {
                this._comingSoonFeature = (string)this.data;
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

    private void LoadUIComponents()
    {
        this.tmp_feature.text = this._comingSoonFeature;
    }

    public void PlayDialogBodyOnShowAnim()
    {
        this.panel_dialog.transform.DOScaleY(1f, 0.3f);
    }

    public void PlayDialogBodyOnCloseAnim()
    {
        this.panel_dialog.transform
            .DOScaleY(0f, 0.3f)
            .OnComplete(this.OnCompleteHide);
    }
}
