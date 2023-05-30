using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRisingSea : CBaseTrap
{
    public float speedConfig = 0.1f;

    private bool _isPlaying;

    private void OnEnable()
    {
        CGameplayManager.Instance.AssignOnGameOverCallback(this.OnGameOver);
    }

    private void OnDisable()
    {
        CGameplayManager.Instance.UnAssignOnGameOverCallback(this.OnGameOver);
    }

    private void Start()
    {
        this._isPlaying = true;
        this.speed = speedConfig;
    }

    private void Update()
    {
        if (this._isPlaying) this.Rise();
    }

    private void Rise()
    {
        this.transform.localScale += Vector3.up * this.speed * Time.deltaTime;
    }

    private void OnGameOver()
    {
        this._isPlaying = false;
    }
}
