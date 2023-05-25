using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundBase : MonoBehaviour
{
    public AudioSource _audioSource;

    public float Play(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            Debug.LogError("KEY NULL: " + key);
            return 0.0f;
        }

        if (this._audioSource != null)
        {
            this._audioSource.clip = CSoundConfigs.Instance.GetAudioByName(key);
            this._audioSource.loop = false;
            this._audioSource.mute = false;

            this._audioSource.Play();

            return this._audioSource.clip.length;
        }

        return 0.0f;
    }

    public void PlayLoop(string key)
    {
        this.Play(key);
        this._audioSource.loop = true;
    }

    public void Mute()
    {
        if (this._audioSource == null)
        {
            return;
        }
        this._audioSource.mute = true;
    }

    public void Stop()
    {
        if (this._audioSource == null)
        {
            return;
        }
        this._audioSource.Stop();
    }
}
