using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CSpikeM : CBaseTrap
{
    public CSpikeMVisual visual;

    private bool _isSpikeOn;

    private void Start()
    {
        this._isSpikeOn = false;
    }

    public void OnPlayerCollided()
    {
        if (!this._isSpikeOn)
        {
            this.visual.PlayAnimationSpikeM();
        }
        else
        {
            CGameplayManager.Instance.OnPlayerHitSpikeMOn();
        }
    }

    public void ChangeSpikeState(bool isOn)
    {
        this._isSpikeOn = isOn;
    }

    public void SetUpSpikeDirection(MapTrapType id)
    {
        switch (id)
        {
            case MapTrapType.SPIKE_M_U:
                this.transform.Rotate(Vector3.forward * 90.0f); break;
            case MapTrapType.SPIKE_M_L:
                this.transform.Rotate(Vector3.forward * 0.0f); break;
            case MapTrapType.SPIKE_M_D:
                this.transform.Rotate(Vector3.forward * 270.0f); break;
            case MapTrapType.SPIKE_M_R:
                this.transform.Rotate(Vector3.forward * 180.0f); break;
        }
    }
}
