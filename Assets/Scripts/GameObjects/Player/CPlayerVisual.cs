using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class CPlayerVisual : CBaseVisual
{
    [SerializeField] private CPlayer _player;
    [SerializeField] private GameObject _playerShield;

    public const string PLAYER_START_GAME_ANIM = "player_start_game";
    public const string PLAYER_IDLE_ANIM = "player_idle";
    public const string PLAYER_JUMP_ANIM = "player_jump";
    public const string PLAYER_DIE_ANIM = "player_die";

    private const string TWEEN = "_FADE_";

    private void OnDisable()
    {
        DOTween.Kill(this.GetInstanceID() + TWEEN);    
    }

    private void LateUpdate()
    {
        this.ReCheckPlayerMovingAnimation();        
    }

    private void ReCheckPlayerMovingAnimation()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_JUMP_ANIM) &&
            !this.animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_IDLE_ANIM)) return;

        if (this._player.IsMoving() && !this.animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_JUMP_ANIM))
        {
            this.animator.Play(PLAYER_JUMP_ANIM);
        }

        if (!this._player.IsMoving() && !this.animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_IDLE_ANIM))
        {
            this.animator.Play(PLAYER_IDLE_ANIM);
        }
    }

    public void PlayStartAnimation()
    {
        this.spriteRenderer.DOFade(1.0f, 2.0f).SetId(this.GetInstanceID() + TWEEN);
        this.animator.Play(PLAYER_START_GAME_ANIM);
    }    

    public void PlayAnimation(string key)
    {
        this.animator.Play(key);
    }

    public void OnPlayerDieAnimCompleted()
    {
        this._player.gameObject.SetActive(false);
    }

    public void OnPlayerShieldStateChanged(bool isOn)
    {
        this._playerShield.SetActive(isOn);
    }
}
