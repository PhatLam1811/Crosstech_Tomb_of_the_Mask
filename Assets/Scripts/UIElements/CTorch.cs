using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTorch : MonoBehaviour
{
    public Animator _animator;

    private float counter = 0.0f;
    
    private bool isDone = false;

    private const string TORCH_FADE_ANIM = "torch_fade";

    private void Update()
    {
        if (this.isDone) return;

        this.counter += Time.deltaTime;
        if (this.counter >= 1.05f)
        {
            this._animator.Play(TORCH_FADE_ANIM);
        }
    }
}
