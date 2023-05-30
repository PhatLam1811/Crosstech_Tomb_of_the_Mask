using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRisingSea : CBaseTrap
{
    public float speedConfig = 0.1f;

    private bool _isPlaying;

    private const float ON_PLAYER_REVIVED_SCALE_DOWN = 2.0f;

    private void OnEnable()
    {
        CGameplayManager.Instance.AssignOnPlayerStateChangedCallback(this.OnPlayerStateChanged);
    }

    private void OnDisable()
    {
        CGameplayManager.Instance.UnAssignOnPlayerStateChangedCallback(this.OnPlayerStateChanged);
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

    private void OnPlayerStateChanged(PlayerState playerState)
    {
        switch(playerState)
        {
            case PlayerState.IS_PLAYING:
                this.SetIsPlaying(true); break;
            case PlayerState.GAME_OVER:
                this.SetIsPlaying(false); break;
            case PlayerState.REVIVED:
                this.OnPlayerRevived(); break;
        }
    }

    private void SetIsPlaying(bool isPlaying)
    {
        this._isPlaying = isPlaying;
    }

    private void OnPlayerRevived()
    {
        this.transform.localScale += Vector3.down * ON_PLAYER_REVIVED_SCALE_DOWN;
        this.SetIsPlaying(true);
    }
}
