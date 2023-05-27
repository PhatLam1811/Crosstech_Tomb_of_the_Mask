using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapClearedDialogData
{
    public int _mapId;

    public bool isCleared;
    public float percentDotsCollected;
    public int starsCollected;
}

public class CGameplayManager : MonoSingleton<CGameplayManager>
{
    private CPlayer _player;

    private int _onPlayingMapId = 1;

    private int collectedDots = 0;
    private int collectedStars = 0;

    private void Update()
    {
        this.PseudoInputProcess();
    }

    public void PlayMap(int id)
    {
        this._onPlayingMapId = id;
        CGameManager.Instance.LoadSceneAsync(GameDefine.PLAY_SCENE_ID);
    }

    public void StartGame(CPlayer player)
    {
        this._player = player;
        this._player.StartGame();
        CGameplayUIManager.Instance.StartGame();
    }

    public int GetOnPlayingMapId()
    {
        return this._onPlayingMapId;
    }

    public CPlayer GetPlayer()
    {
        return this._player;
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

    public void OnPlayerHitDotGame(CDotGame collectedDotGame)
    {
        this.collectedDots++;
        collectedDotGame.OnCollectedByPlayer();
    }

    public void OnPlayerHitStar(CStar collectedStar) 
    {
        this.collectedStars++;
        collectedStar.OnCollectedByPlayer();
        CGameplayUIManager.Instance.OnPlayerCollectedStar(this.collectedStars);
    }

    public void OnPlayerHitCoin(CCoin collectedCoin)
    {
        collectedCoin.OnCollectedByPlayer();
        CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.ADD_VALUE, BoosterType.COIN, 1);
    }

    public void OnPlayerReachExit()
    {
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.SET_IS_CLEARED, this._onPlayingMapId);
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.SET_GAME_MAP_STARS, this._onPlayingMapId, this.collectedStars);
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.UNLOCK_MAP, this._onPlayingMapId + 1);
        CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.ADD_VALUE, BoosterType.GAMEDOT, this.collectedDots);
        
        this.ShowMapClearedDialog();
    }

    private void ShowMapClearedDialog()
    {
        int currentMapTotalDots = CPlayMapManager.Instance.GetMapTotalDots();
        float percentDotsCollected = (this.collectedDots * 1.0f) / currentMapTotalDots;

        CMapClearedDialogData result = new CMapClearedDialogData()
        {
            _mapId = this._onPlayingMapId,
            isCleared = true,
            percentDotsCollected = percentDotsCollected,
            starsCollected = this.collectedStars
        };

        CGameDialogManager.Instance.ShowDialog<CMapClearedDialog>(
            path: GameDefine.DIALOG_MAP_CLEARED_PATH,
            canvasPos: CGameplayUIManager.Instance.GetCanvasPos(),
            data: result);
    }
}
