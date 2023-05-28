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

    private int rotationZ = 0;

    public const string PLAYER_START_GAME_ANIM = "player_start_game";
    public const string PLAYER_IDLE_ANIM = "player_idle";
    public const string PLAYER_JUMP_ANIM = "player_jump";
    public const string PLAYER_DIE_ANIM = "player_die";

    public void PlayStartAnimation()
    {
        this.spriteRenderer.DOFade(1.0f, 2.0f);
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

    public int GetRotationZ()
    {
        return this.rotationZ;
    }

    public void RotateZ(int value)
    {
        this.rotationZ = value;
        this.transform.DORotate(Vector3.forward * value, 0f);
    }

    public void RotateZInverse()
    {
        this.rotationZ = (this.rotationZ + 180) % 360;
        this.transform.DORotate(Vector3.forward * this.rotationZ, 0f);
    }

    public void RotateX(int value)
    {
        this.transform.DORotate(Vector3.right * value, 0f);
    }
}
