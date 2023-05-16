using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePlayManager : MonoSingleton<CGamePlayManager>
{
    public CPlayer _player;

    public Grid _grid;

    public GameObject prefab_dotGame;
    public GameObject prefab_star;
    public GameObject prefab_coin;

    private void Update()
    {
        this.PseudoInputProcess();
    }

    public void StartGame()
    {
        this.LoadLevelMap();
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

    private void LoadLevelMap()
    {
        CLevelConfig levelConfig = CLevelConfigs.Instance._levelConfigs[0];

        foreach (CCollectableObjectPositionConfig config in levelConfig._collectableObjectPositionConfigs)
        {
            Vector3Int cellPos = new Vector3Int(config.x, config.y, 0);

            Vector3 worldPos = _grid.CellToWorld(cellPos);

            switch (config._id)
            {
                case GameDefine.DOT_TILE_ID:
                    Instantiate(this.prefab_dotGame, worldPos, Quaternion.identity); break;
                case GameDefine.STAR_TILE_ID:
                    Instantiate(this.prefab_star, worldPos, Quaternion.identity); break;
                case GameDefine.COIN_TILE_ID:
                    Instantiate(this.prefab_coin, worldPos, Quaternion.identity); break;
            }
        }
    }

    public void OnPlayerHitDotGame(CDotGame collidedDotGame)
    {
        collidedDotGame.OnCollectedByPlayer();
    }

    public void OnPlayerHitStar() { }

    public void OnPlayerHitCoin() { }
}
