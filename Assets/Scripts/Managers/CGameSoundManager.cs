using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CGameSoundManager : MonoSingleton<CGameSoundManager>
{
    public CSoundBase _fxSoundBase;
    public CSoundBase _playerFxSoundBase;
    public CSoundBase _bgmSoundBase;

    private bool isFxOn;
    private bool isBGMOn;

    public void OpenApp()
    {
        this.isFxOn = CGameDataManager.Instance.GetGameSettingsData().isFxOn;
        this.isBGMOn = CGameDataManager.Instance.GetGameSettingsData().isBGMOn;

        CGameSettingsData.Instance.AssignOnSettingChangedCallback(this.OnGameSoundSettingsChanged);
    }

    #region Fx
    public void PlayFx(string key)
    {
        this.Play(key, this._fxSoundBase, !this.isFxOn);
    }

    public void PlayLoopFx(string key)
    {
        this.PlayLoop(key, this._fxSoundBase, !this.isFxOn);
    }

    public void MuteFx()
    {
        this.Mute(_fxSoundBase, !this.isFxOn);
    }

    public void StopFx()
    {
        this.Stop(_fxSoundBase);
    }
    #endregion

    #region PlayerFx
    public void PlayPlayerFx(string key)
    {
        this.Play(key, this._playerFxSoundBase, !this.isFxOn);
    }

    public void PlayLoopPlayerFx(string key)
    {
        this.PlayLoop(key, this._playerFxSoundBase, !this.isFxOn);
    }

    public void MutePlayerFx()
    {
        this.Mute(_playerFxSoundBase, !this.isFxOn);
    }

    public void StopPlayerFx()
    {
        this.Stop(_playerFxSoundBase);
    }
    #endregion

    #region BGM
    public void PlayBGM(string key)
    {
        this.Play(key, this._bgmSoundBase, !this.isBGMOn);
    }

    public void PlayLoopBGM(string key)
    {
        this.PlayLoop(key, this._bgmSoundBase, !this.isBGMOn);
    }

    public void MuteBGM()
    {
        this.Mute(_bgmSoundBase, !this.isBGMOn);
    }

    public void StopBGM()
    {
        this.Stop(_bgmSoundBase);
    }
    #endregion

    #region Common
    private void Play(string key, CSoundBase soundBase, bool isMute)
    {
        if (soundBase != null)
        {
            soundBase.Play(key, isMute);
        }
    }

    private void PlayLoop(string key, CSoundBase soundBase, bool isMute)
    {
        if (soundBase != null)
        {
            soundBase.PlayLoop(key, isMute);
        }
    }

    private void Mute(CSoundBase soundBase, bool isMute)
    {
        if (soundBase != null)
        {
            soundBase.Mute(isMute);
        }
    }

    private void Stop(CSoundBase soundBase)
    {
        if (soundBase != null)
        {
            soundBase.Stop();
        }
    }
    
    public void OnGameSoundSettingsChanged(SettingsType type, bool isOn)
    {
        Debug.Log("Is Triggered : " + isOn);

        switch (type)
        {
            case SettingsType.BGM_SETTINGS:
                this.isBGMOn = isOn;
                
                this.Mute(this._bgmSoundBase, !this.isBGMOn);
                
                break;
            case SettingsType.FX_SETTINGS:
                this.isFxOn = isOn;

                this.Mute(this._fxSoundBase, !this.isFxOn);
                this.Mute(this._playerFxSoundBase, !this.isFxOn);
                
                break;
        }
    }
    #endregion
}
