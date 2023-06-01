using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShopDialog : CBaseDialog
{
    private new void OnEnable() { }

    public override void OnHide()
    {
        this.OnCompleteHide();
    }
}
