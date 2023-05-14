using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoSingleton<CGameManager>
{
    private void Start()
    {
        this.StartGame();
    }

    void StartGame()
    {
        // load Game Configs
        // load Game Data
        CGamePlayManager.Instance.StartGame();
    }

    public T GetResourceFile<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path) as T;
    }
}
