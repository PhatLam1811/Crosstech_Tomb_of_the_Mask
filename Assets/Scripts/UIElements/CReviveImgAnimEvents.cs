using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CReviveImgAnimEvents : CUIAnimationEvents
{
    public CReviveDialog _reviveDialog;

    public override void OnAnimationComplete()
    {
        if (!this._reviveDialog.GetIsReivived()) this._reviveDialog.OnHide();
    }

    public void OnReviveSuccessAnimComplete()
    {
        this._reviveDialog.PlayDialogBodyOnCloseAnim(true);
    }
}
