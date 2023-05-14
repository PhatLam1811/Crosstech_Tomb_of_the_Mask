using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePlayManager : MonoSingleton<CGamePlayManager>
{
    [SerializeField] private CPlayer _player;

    private void Update()
    {
        this.PseudoInputProcess();
    }

    // called after game data is fully loaded
    public void StartGame()
    {
        this._player.StartGame();
    }

    public void PseudoInputProcess()
    {
        if (Input.GetKeyDown(KeyCode.W))
            this._player.RegisterNextMove(PlayerMoves.Up);

        if (Input.GetKeyDown(KeyCode.A))
            this._player.RegisterNextMove(PlayerMoves.Left);

        if (Input.GetKeyDown(KeyCode.S))
            this._player.RegisterNextMove(PlayerMoves.Down);

        if (Input.GetKeyDown(KeyCode.D))
            this._player.RegisterNextMove(PlayerMoves.Right);
    }
}
