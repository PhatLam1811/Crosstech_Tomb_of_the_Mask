using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameDot : CBaseCollectableObject
{
    public override void OnCollectedByPlayer()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.DOT_PICK_UP_FX_KEY);
        CGameplayManager.Instance.OnPlayerCollectGameDot();
        base.OnCollectedByPlayer();
    }
}
