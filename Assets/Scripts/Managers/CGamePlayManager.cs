using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePlayManager : MonoSingleton<CGamePlayManager>
{
    private void Update()
    {
    }

    // called after game data is fully loaded
    public void StartGame()
    {
        Debug.Log("Game started!");

        CInputManager.Instance.StartGame();
    }
}
