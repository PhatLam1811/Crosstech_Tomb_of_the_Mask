using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePlayManager : MonoSingleton<CGamePlayManager>
{
    [SerializeField] private CPlayer _player;

    private void Update()
    {
    }

    // called after game data is fully loaded
    public void StartGame()
    {

    }
}
