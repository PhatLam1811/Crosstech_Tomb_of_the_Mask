using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoinVisual : CBaseVisual
{
    [SerializeField] private CCoin _coin;
    
    private const string COIN_COLLECTED_ANIM = "coin_pick_anim";

    public void PlayCollectedAnim()
    {
        this.animator.Play(COIN_COLLECTED_ANIM);
    }

    public void OnCollectedAnimCompleted()
    {
        Destroy(this._coin.gameObject);
    }
}
