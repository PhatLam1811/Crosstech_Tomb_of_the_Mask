using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIce : CBaseGameObject
{
    public CIceVisual _visual;

    public void OnCollidedWithPlayer()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.ICE_BREAK_FX_KEY);
        this._visual.OnBrokeByPlayer();
    }
}
