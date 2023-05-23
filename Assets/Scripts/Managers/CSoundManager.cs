using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundManager : MonoSingleton<CSoundManager>
{
    public CSoundBase _fxSoundBase;
    public CSoundBase _bgmSoundBase;

    #region Fx
    public void PlayFx(string key)
    {
        this.Play(key, _fxSoundBase);
    }

    public void PlayLoopFx(string key)
    {
        this.PlayLoop(key, _fxSoundBase);
    }

    public void MuteFx()
    {
        this.Mute(_fxSoundBase);
    }

    public void StopFx()
    {
        this.Stop(_fxSoundBase);
    }
    #endregion

    #region BGM
    public void PlayBGM(string key)
    {
        this.Play(key, _bgmSoundBase);
    }

    public void PlayLoopBGM(string key)
    {
        this.PlayLoop(key, _bgmSoundBase);
    }

    public void MuteBGM()
    {
        this.Mute(_bgmSoundBase);
    }

    public void StopBGM()
    {
        this.Stop(_bgmSoundBase);
    }
    #endregion

    #region Common
    private void Play(string key, CSoundBase soundBase)
    {
        if (soundBase != null)
        {
            soundBase.Play(key);
        }    
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
