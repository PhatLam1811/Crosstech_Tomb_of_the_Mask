using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class CGameManager : MonoSingleton<CGameManager>
{
    private void Start()
    {
        this.OpenApp();
    }

    private void Update()
    {
        this.OnGameManageKeyInputProcess();
    }

    private void OpenApp()
    {
        DontDestroyOnLoad(this.gameObject);

        CGameDataManager.Instance.OpenApp();
        CLoadingSceneManager.Instance.OpenApp();
    }

    public void OnGameManageKeyInputProcess()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CGameDataManager.Instance.ClearGameData();
        }
    }

    public void LoadSceneAsync(int key)
    {
        SceneManager.LoadSceneAsync(key);
    }

    public void LoadScene(int key)
    {
        SceneManager.LoadScene(key);
    }

    public void ShowDialog<T>(string path, Transform canvasPos, object data = null, UnityAction callbackCompleteShow = null) where T : CBaseDialog
    {
        GameObject dialogPrefab = CGameManager.Instance.GetResourceFile<GameObject>(path);

        if (dialogPrefab != null)
        {
            T dialogComponent = (Instantiate(dialogPrefab, canvasPos)).GetComponent<T>();

            if (dialogComponent != null)
            {
                dialogComponent.OnShow(data, callbackCompleteShow);
            }
        }
    }

    public T GetResourceFile<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path) as T;
    }
}
