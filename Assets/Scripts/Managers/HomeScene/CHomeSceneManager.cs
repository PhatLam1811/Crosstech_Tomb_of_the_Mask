using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHomeSceneManager : MonoSingleton<CHomeSceneManager>
{
    public CStoryModeDialog dialog_story_mode;

    private float bgm_countdown = 0.0f;
    private bool _isPlayBGM = false;

    private const float BGM_START_POINT = 1.0f;

    private void Start()
    {
        this.LoadScene();
    }

    private void Update()
    {
        if (!this._isPlayBGM && this.bgm_countdown >= BGM_START_POINT)
        {
            this._isPlayBGM = true;
            CGameSoundManager.Instance.PlayLoopBGM(GameDefine.HOME_SCENE_BGM_KEY);
        }
        
        if (!this._isPlayBGM)
        {
            this.bgm_countdown += Time.deltaTime;
        }
    }

    public void LoadScene()
    {
        CHomeSceneUIManager.Instance.LoadScene();
    }

    public void LoadNavigateDialog()
    {
        this.dialog_story_mode.OnShow();
    }
}
