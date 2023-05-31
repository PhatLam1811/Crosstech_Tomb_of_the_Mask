using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpikeMVisual : CBaseVisual
{
    public CSpikeM _spikeM; 

    private const string SPIKE_M_ON_ANIM = "spike_m";

    public void PlayAnimationSpikeM()
    {
        this.animator.Play(SPIKE_M_ON_ANIM);
    }

    public void OnSpikeMFullyOn()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.SPIKE_M_ON_FX_KEY);   
        this._spikeM.ChangeSpikeState(true);
    }

    public void OnSpikeOff()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.SPIKE_M_OFF_FX_KEY);
        this._spikeM.ChangeSpikeState(false);
    }
}
