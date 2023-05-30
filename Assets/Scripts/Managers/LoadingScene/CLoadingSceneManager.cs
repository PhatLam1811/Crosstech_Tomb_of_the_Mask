using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CLoadingSceneManager : MonoSingleton<CLoadingSceneManager>
{
    public Image img_loading_logos;
    public Image img_playgendary_logo;
    public Image img_my_logo;
    public Image img_shelzy_1_logo;
    public Image img_screen_mask;
    public Button btn_tap_to_play;

    public GameObject game_title;

    public List<GameObject> scenery_objects;

    public CCameraViewPortHandler viewPortHandler;

    public void OpenApp()
    {
        this.PlayLoadingScenePhase1();
    }

    private void PlayLoadingScenePhase1()
    {
        float loadingLogosFadeDuration = 1.0f;
        float loadingLogosDisplayDuration = 0.5f;

        Sequence phase1Sequence = DOTween.Sequence();

        phase1Sequence.OnStart(() => this.img_loading_logos.gameObject.SetActive(true));
        
        phase1Sequence.Join(this.img_playgendary_logo.DOFade(1.0f, loadingLogosFadeDuration));
        phase1Sequence.Join(this.img_my_logo.DOFade(1.0f, loadingLogosFadeDuration));
        phase1Sequence.Join(this.img_shelzy_1_logo.DOFade(1.0f, loadingLogosFadeDuration));
        
        phase1Sequence.AppendInterval(loadingLogosDisplayDuration);
        
        phase1Sequence.Join(this.img_playgendary_logo.DOFade(0.0f, loadingLogosFadeDuration));
        phase1Sequence.Join(this.img_my_logo.DOFade(0.0f, loadingLogosFadeDuration));
        phase1Sequence.Join(this.img_shelzy_1_logo.DOFade(0.0f, loadingLogosFadeDuration));

        phase1Sequence.AppendCallback(() => this.img_loading_logos.gameObject.SetActive(false));

        phase1Sequence.OnComplete(this.PlayLoadingScenePhase2);
    }

    private void PlayLoadingScenePhase2()
    {
        StartCoroutine(this.couroutineLoadingScenePhase2());
    }

    private IEnumerator couroutineLoadingScenePhase2()
    {
        float titleDelay = 1.5f;
        this.ShowGameTitle();

        yield return new WaitForSeconds(titleDelay);

        float sceneryDelay = 2.0f;
        this.ShowScenery();

        yield return new WaitForSeconds(sceneryDelay);

        this.ShowBtnTapToPlay();
    }

    private void ShowGameTitle()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.TITLE_REVEAL_FX_KEY);
        this.game_title.SetActive(true);
    }

    private void ShowScenery()
    {
        CGameSoundManager.Instance.PlayBGM(GameDefine.LOADING_SCENE_WATER_BGM_KEY);

        foreach (GameObject sceneryObj in this.scenery_objects)
        {
            sceneryObj.SetActive(true);
        }
    }

    private void ShowBtnTapToPlay()
    {
        this.btn_tap_to_play.gameObject.SetActive(true);
    }

    public void OnBtnTapToPlayClicked()
    {
        float cameraZoomInDuration = 1.5f;

        CGameSoundManager.Instance.PlayFx(GameDefine.TAP_TO_PLAY_FX_KEY);
        CGameSoundManager.Instance.StopBGM();

        Camera.main.transform.DOMoveZ(6f, cameraZoomInDuration).OnComplete(this.LoadHomeScene);

        this.btn_tap_to_play.gameObject.SetActive(false);
    }

    public void LoadHomeScene()
    {
        CGameManager.Instance.LoadSceneAsync(GameDefine.HOME_SCENE_ID);
    }
}
