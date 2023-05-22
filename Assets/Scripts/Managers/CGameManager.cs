using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CGameManager : MonoSingleton<CGameManager>
{
    public GameObject _playgendaryLogo;
    public GameObject _myLogo;
    public GameObject _shelzyLogo;

    public GameObject[] _sceneryObjects;

    public Animator _titleAnimator;

    public TextMeshProUGUI _tapToPlayButton;

    private void Start()
    {
        this.OpenApp();
    }

    void OpenApp()
    {
        // load Game Configs
        // load Game Data
        // CGamePlayManager.Instance.StartGame();
        this.PlayLoadingScene();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayLoadingScene()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_playgendaryLogo.GetComponent<SpriteRenderer>().DOFade(1f, 2f));
        seq.Join(_myLogo.GetComponent<SpriteRenderer>().DOFade(1f, 2f));
        seq.Join(_shelzyLogo.GetComponent<SpriteRenderer>().DOFade(1f, 2f));
        seq.AppendInterval(3f);
        seq.Append(_playgendaryLogo.GetComponent<SpriteRenderer>().DOFade(0f, 2f));
        seq.Join(_myLogo.GetComponent<SpriteRenderer>().DOFade(0f, 2f));
        seq.Join(_shelzyLogo.GetComponent<SpriteRenderer>().DOFade(0f, 2f));
        seq.AppendCallback(() =>
        {
            CSoundManager.Instance.PlayFx(GameDefine.TITLE_REVEAL_FX_KEY);
            _titleAnimator.Play(GameDefine.TITLE_REVEAL_ANIM);
        });
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            CSoundManager.Instance.PlayLoopBGM(GameDefine.START_SCENE_WATER_BGM_KEY);
        });

        foreach (GameObject sceneryObject in _sceneryObjects)
        {
            seq.Join(sceneryObject.GetComponent<SpriteRenderer>().DOFade(1f, 2f));
        }

        seq.Join(_tapToPlayButton.DOFade(1f, 2f));

        seq.Play();
    }

    public void OnTapToPlayClicked()
    {
        SceneManager.LoadSceneAsync(GameDefine.PLAY_SCENE_ID);
        // CGamePlayManager.Instance.StartGame();
    }

    public T GetResourceFile<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path) as T;
    }
}
