using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHomeSceneManager : MonoSingleton<CHomeSceneManager>
{
    private void Start()
    {
        this.LoadScene();
    }

    public void LoadScene()
    {
        this.PlayHomeSceneBGM();
        CHomeSceneUIManager.Instance.LoadScene();
    }

    public void PlayHomeSceneBGM()
    {
        CGameSoundManager.Instance.PlayLoopBGM(GameDefine.HOME_SCENE_BGM_KEY);
    }
}
