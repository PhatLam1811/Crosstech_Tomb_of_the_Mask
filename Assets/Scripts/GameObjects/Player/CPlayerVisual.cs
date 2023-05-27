using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class CPlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private CPlayer _player;

    private int rotationZ = 0;

    public const string PLAYER_START_GAME_ANIM = "player_start_game";
    public const string PLAYER_IDLE_ANIM = "player_idle";
    public const string PLAYER_JUMP_ANIM = "player_jump";

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        this.animator = this.GetComponent<Animator>();
    }
#endif

    public void PlayAnimation(string key)
    {
        this.animator.Play(key);
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
