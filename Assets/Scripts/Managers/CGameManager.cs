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
}
