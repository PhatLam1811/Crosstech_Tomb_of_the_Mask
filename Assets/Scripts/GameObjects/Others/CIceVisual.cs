using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIceVisual : CBaseVisual
{
    public CIce _ice;

    private const string ICE_BROKEN_ANIM = "ice_break";

    public void OnBrokeByPlayer()
    {
        this.animator.Play(ICE_BROKEN_ANIM);
    }

    public void OnAnimationBrokenComplete()
    {
        Destroy(this._ice.gameObject);
    }
}
