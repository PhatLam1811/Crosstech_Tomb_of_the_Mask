using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseVisual : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        this.animator = this.GetComponent<Animator>();
    }
#endif
}
