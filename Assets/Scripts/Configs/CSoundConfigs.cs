using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Sound Configs", fileName = "SoundConfigs")]
public class CSoundConfigs : ScriptableObject
{
    #region Singleton
    private static CSoundConfigs _instance;

    public static CSoundConfigs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CGameManager.Instance.GetResourceFile<CSoundConfigs>(GameDefine.SOUND_CONFIGS_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(CSoundConfigs).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    public List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> _audioClipsDictionary;

    public AudioClip GetAudioByName(string key)
    {
        this._audioClipsDictionary ??= new Dictionary<string, AudioClip>();

        if (this._audioClipsDictionary.TryGetValue(key, out AudioClip clipFromDictionary))
        {
            return clipFromDictionary;
        }

        AudioClip audioClip = this.audioClips.Find(x => x.name.Equals(key));

        if (audioClip != null)
        {
            this._audioClipsDictionary.Add(key, audioClip);
            return audioClip;
        }
        else
        {
            Debug.LogError("Can't find required sound file: " + key);
            return null;
        }
    }
}
