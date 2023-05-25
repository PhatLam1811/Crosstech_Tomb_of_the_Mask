using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CGameSoundManager : MonoSingleton<CGameSoundManager>
{
    public CSoundBase _fxSoundBase;
    public CSoundBase _bgmSoundBase;

    private UnityAction<bool> callbackOnFxIsPlayingChange;
    private UnityAction<bool> callbackOnBGMIsPlayingChange;

    private bool isFxPlaying = false;
    private bool isBGMPlaying = false;

    private bool isFxLoop = false;
    private bool isBGMLoop = false;

    private float fxClipLength = 0.0f;
    private float bgmClipLength = 0.0f;

    private float fxPlayedTime = 0.0f;
    private float bgmPlayedTime = 0.0f;

    private void Update()
    {
        this.IsFxEnd();
        this.IsBGMEnd();
    }

    #region Fx
    public void PlayFx(string key)
    {
        this.isFxLoop = false;
        this.fxPlayedTime = 0.0f;
        this.fxClipLength = this.Play(key, _fxSoundBase);
        this.InvokeCallbackOnFxIsPlayingChange(true);
    }

    public void PlayLoopFx(string key)
    {
        this.isFxLoop = true;
        this.PlayLoop(key, _fxSoundBase);
        this.InvokeCallbackOnFxIsPlayingChange(true);
    }

    public void MuteFx()
    {
        this.Mute(_fxSoundBase);
        this.InvokeCallbackOnFxIsPlayingChange(false);
    }

    public void StopFx()
    {
        this.Stop(_fxSoundBase);
        this.InvokeCallbackOnFxIsPlayingChange(false);
    }

    private void IsFxEnd()
    {
        if (!this.isFxLoop && this.fxClipLength > 0.0f)
        {
            this.fxPlayedTime += Time.deltaTime;

            if (this.fxPlayedTime >= this.fxClipLength)
            {
                this.isFxLoop = false;
                this.fxClipLength = 0.0f;
                this.fxPlayedTime = 0.0f;
                this.InvokeCallbackOnFxIsPlayingChange(false);
            }
        }
    }

    public void AssignCallbackOnFxIsPlayingChange(UnityAction<bool> callback)
    {
        this.callbackOnFxIsPlayingChange -= callback;
        this.callbackOnFxIsPlayingChange += callback;
    }

    public void UnAssignCallbackOnFxIsPlayingChange(UnityAction<bool> callback)
    {
        this.callbackOnFxIsPlayingChange -= callback;
    }

    public void InvokeCallbackOnFxIsPlayingChange(bool isPlaying)
    {
        this.isFxPlaying = isPlaying;
        this.callbackOnFxIsPlayingChange?.Invoke(this.isFxPlaying);
    }
    #endregion

    #region BGM
    public void PlayBGM(string key)
    {
        this.isBGMLoop = false;
        this.bgmPlayedTime = 0.0f;
        this.bgmClipLength = this.Play(key, _bgmSoundBase);
        this.InvokeCallbackOnBGMIsPlayingChange(true);
    }

    public void PlayLoopBGM(string key)
    {
        this.isBGMLoop = true;
        this.PlayLoop(key, _bgmSoundBase);
        this.InvokeCallbackOnBGMIsPlayingChange(true);
    }

    public void MuteBGM()
    {
        this.Mute(_bgmSoundBase);
        this.InvokeCallbackOnBGMIsPlayingChange(false);
    }

    public void StopBGM()
    {
        this.Stop(_bgmSoundBase);
        this.InvokeCallbackOnBGMIsPlayingChange(false);
    }

    public void IsBGMEnd()
    {
        if (!this.isBGMLoop && this.bgmClipLength > 0.0f)
        {
            this.bgmPlayedTime += Time.deltaTime;

            if (this.bgmPlayedTime >= this.bgmClipLength)
            {
                this.isBGMLoop = false;
                this.bgmClipLength = 0.0f;
                this.bgmPlayedTime = 0.0f;
                this.InvokeCallbackOnBGMIsPlayingChange(false);
            }
        }
    }

    public void AssignCallbackOnBGMIsPlayingChange(UnityAction<bool> callback)
    {
        this.callbackOnBGMIsPlayingChange -= callback;
        this.callbackOnBGMIsPlayingChange += callback;
    }

    public void UnAssignCallbackOnBGMIsPlayingChange(UnityAction<bool> callback)
    {
        this.callbackOnBGMIsPlayingChange -= callback;
    }

    public void InvokeCallbackOnBGMIsPlayingChange(bool isPlaying)
    {
        this.isBGMPlaying = isPlaying;
        this.callbackOnBGMIsPlayingChange?.Invoke(this.isBGMPlaying);
    }
    #endregion

    #region Common
    private float Play(string key, CSoundBase soundBase)
    {
        if (soundBase != null)
        {
            return soundBase.Play(key);
        }

        return 0.0f;
    }

    private void PlayLoop(string key, CSoundBase soundBase)
    {
        if (soundBase != null)
        {
            soundBase.PlayLoop(key);
        }
    }

    private void Mute(CSoundBase soundBase)
    {
        if (soundBase != null)
        {
            soundBase.Mute();
        }
    }

    private void Stop(CSoundBase soundBase)
    {
        if (soundBase != null)
        {
            soundBase.Stop();
        }
    }
    #endregion
}
