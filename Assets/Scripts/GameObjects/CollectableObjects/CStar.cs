using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStar : CBaseCollectableObject
{
    public override void OnCollectedByPlayer()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.STAR_PICK_UP_FX_KEY);
        CGameplayManager.Instance.OnPlayerCollectStar();
        base.OnCollectedByPlayer();
    }
}
