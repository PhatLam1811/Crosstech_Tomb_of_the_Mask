using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoin : CBaseCollectableObject
{
    public CCoinVisual _visual;

    public override void OnCollectedByPlayer()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.COIN_PICK_UP_FX_KEY);
        CGameplayManager.Instance.OnPlayerCollectCoin();
        this._visual.PlayCollectedAnim();
    }
}
