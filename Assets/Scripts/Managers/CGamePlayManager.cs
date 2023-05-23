using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameplayManager : MonoSingleton<CGameplayManager>
{
    public CPlayer _player;

    public Grid _grid;

    public GameObject prefab_exit;
    public GameObject prefab_dotGame;
    public GameObject prefab_star;
    public GameObject prefab_coin;

    public int currentMapTotalCoins = 0;

    public int currentMapTotalDots;

    public int currentMapCollectedDots;
    public int currentMapCollectedStars;

    private void Start()
    {
        CGameplayManager.Instance.StartGame();
    }

    private void Update()
    {
        this.PseudoInputProcess();
    }

    public void StartGame()
    {
        this.LoadLevelMap();

        CGameplayUIManager.Instance.StartGame();

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
        CMapConfig levelConfig = CMapConfigs.Instance._mapConfigs[0];

        foreach (CCollectableObjectPositionConfig config in levelConfig._collectableObjectPositionConfigs)
        {
            Vector3Int cellPos = new Vector3Int(config.x, config.y, 0);

            Vector3 worldPos = _grid.CellToWorld(cellPos);

            switch (config._id)
            {
                case GameDefine.DOT_TILE_ID:
                    int min = 1;
                    int max = 10;
                    if (this.currentMapTotalCoins * 10 >= levelConfig._collectableObjectPositionConfigs.Count || Random.Range(min, max) != 1)
                    {
                        Instantiate(this.prefab_dotGame, worldPos, Quaternion.identity);
                        this.currentMapTotalDots++;
                    }    
                    else
                    {
                        Instantiate(this.prefab_coin, worldPos, Quaternion.identity);
                        this.currentMapTotalCoins++;
                    }
                    break;
                case GameDefine.STAR_TILE_ID:
                    Instantiate(this.prefab_star, worldPos, Quaternion.identity); break;
                case GameDefine.EXIT_TILE_ID:
                    Instantiate(this.prefab_exit, worldPos, Quaternion.identity); break;
            }
        }
    }

    public void OnPlayerHitDotGame(CDotGame collectedDotGame)
    {
        this.currentMapCollectedDots++;
        collectedDotGame.OnCollectedByPlayer();
    }

    public void OnPlayerHitStar(CStar collectedStar) 
    {
        this.currentMapCollectedStars++;
        collectedStar.OnCollectedByPlayer();
    }

    public void OnPlayerHitCoin(CCoin collectedCoin)
    {
        collectedCoin.OnCollectedByPlayer();
        CPlayerBoosterDatas.Instance.AddValueBooster(BoosterType.COIN, 1);
    }
}
