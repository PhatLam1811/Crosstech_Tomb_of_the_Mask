using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SettingsType
{
    FX_SETTINGS = 0,
    BGM_SETTINGS = 1
}

[System.Serializable]
public class CGameSettingsData
{
    public static CGameSettingsData Instance => CGameDataManager.Instance._gameData?._gameSettings;

    public bool isBGMOn;
    public bool isFxOn;

    private UnityAction<SettingsType, bool> _onSettingsChangeCallback;

    public void CreateNew()
    {
        this.isBGMOn = true;
        this.isFxOn = true;
    }

    public void ChangeBGMSetting(bool isOn)
    {
        this.isBGMOn = isOn;
        this.InvokeOnSettingsChangedCallback(SettingsType.BGM_SETTINGS, isOn);
    }

    public void ChangeFxSetting(bool isOn)
    {
        this.isFxOn = isOn;
        this.InvokeOnSettingsChangedCallback(SettingsType.FX_SETTINGS, isOn);
    }

    public void AssignOnSettingChangedCallback(UnityAction<SettingsType, bool> callback)
    {
        this._onSettingsChangeCallback -= callback;
        this._onSettingsChangeCallback += callback;
    }

    public void UnAssignOnSettingChangedCallback(UnityAction<SettingsType, bool> callback)
    {
        this._onSettingsChangeCallback -= callback;
    }

    private void InvokeOnSettingsChangedCallback(SettingsType type, bool isOn)
    {
        this._onSettingsChangeCallback?.Invoke(type, isOn);
    }
}
