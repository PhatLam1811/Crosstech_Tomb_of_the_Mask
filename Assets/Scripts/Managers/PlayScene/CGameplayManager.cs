using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapClearedDialogData
{
    public float percentDotsCollected;
    public int starsCollected;
}

public class CGameplayManager : MonoSingleton<CGameplayManager>
{
    private CPlayer _player;

    private int _onPlayingMapId = 1;

    private int collectedDots;
    private int collectedStars;

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
        this.collectedDots = 0;
        this.collectedStars = 0;

        this._player = player;
        this._player.StartGame();

        CGameplayUIManager.Instance.StartGame();
        CGameplayInputManager.Instance.AssignOnPlayerSwipedCallback(this.OnPlayerSwiped);

        CGameSoundManager.Instance.PlayFx(GameDefine.GAME_START_FX_KEY);
        CGameSoundManager.Instance.PlayLoopBGM(GameDefine.GAMEPLAY_BGM_KEY);
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

    public void OnPlayerSwiped(SwipeDirection swipeDirection)
    {
        switch(swipeDirection)
        {
            case SwipeDirection.UP:
                this._player.RegisterNextMove(PlayerMoves.Up); break;
            case SwipeDirection.LEFT:
                this._player.RegisterNextMove(PlayerMoves.Left); break;
            case SwipeDirection.DOWN:
                this._player.RegisterNextMove(PlayerMoves.Down); break;
            case SwipeDirection.RIGHT:
                this._player.RegisterNextMove(PlayerMoves.Right); break;
        }
    }

    public void OnPlayerHitCollectableObject(CBaseCollectableObject obj)
    {
        obj.OnCollectedByPlayer();
    }

    public void OnPlayerCollectGameDot()
    {
        this.collectedDots++;
    }

    public void OnPlayerCollectStar() 
    {
        this.collectedStars++;
        CGameplayUIManager.Instance.OnPlayerCollectedStar(this.collectedStars);
    }

    public void OnPlayerCollectCoin()
    {
        CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.ADD_VALUE, BoosterType.COIN, 1);
    }

    public void OnPlayerReachExit()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.PLAYER_WIN_FX_KEY);
        CGameplayInputManager.Instance.UnAssignOnPlayerSwipedCallback(this.OnPlayerSwiped);
        CGameplayInputManager.Instance.DisableSelf();

        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.SET_IS_CLEARED, this._onPlayingMapId);
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.SET_GAME_MAP_STARS, this._onPlayingMapId, this.collectedStars);
        CGameDataManager.Instance.UpdateGameMapData(GameMapUpdateType.UNLOCK_MAP, this._onPlayingMapId + 1);
        CGameDataManager.Instance.UpdatePlayerBoosterData(BoosterUpdateType.ADD_VALUE, BoosterType.GAMEDOT, this.collectedDots);
        
        this.ShowMapClearedDialog();
    }

    public void OnPlayerShieldStateChanged(bool isActive)
    {
        if (isActive)
            this._player.OnShieldStateChanged(isActive);
        else
            CGameplayUIManager.Instance.OnPlayerShieldDown();
    }

    public void OnPlayerShieldExpired()
    {
        this._player.OnShieldStateChanged(false);
    }

    private void ShowMapClearedDialog()
    {
        int currentMapTotalDots = CGameplayMapManager.Instance.GetMapTotalDots();
        float percentDotsCollected = (this.collectedDots * 1.0f) / currentMapTotalDots;

        CMapClearedDialogData result = new CMapClearedDialogData();
        result.percentDotsCollected = percentDotsCollected;
        result.starsCollected = this.collectedStars;

        CGameManager.Instance.ShowDialog<CMapClearedDialog>(
            path: GameDefine.DIALOG_MAP_CLEARED_PATH,
            canvasPos: CGameplayUIManager.Instance.GetCanvasPos(),
            data: result);
    }
}
