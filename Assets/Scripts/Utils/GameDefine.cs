using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine
{
    // SCENES
    public const int LOADING_SCENE_ID = 0;
    public const int PLAY_SCENE_ID = 1;

    // DATA
    public const string GAME_DATA = "GAME_DATA";

    // CONFIGS
    public const string DIALOG_CONFIGS_FILE_PATH = "Configs/DialogConfigs";
    public const string PLAYER_CONFIGS_FILE_PATH = "Configs/PlayerConfigs";
    public const string MAP_CONFIGS_FILE_PATH = "Configs/MapConfigs";
    public const string SOUND_CONFIGS_FILE_PATH = "Configs/SoundConfigs";

    // ANIMATIONS
    public const string TITLE_REVEAL_ANIM = "title_reveal";

    // TILES
    public const int DOT_TILE_ID = 0;
    public const int STAR_TILE_ID = 1;
    public const int COIN_TILE_ID = 2;
    public const int EXIT_TILE_ID = 3;

    // SOUNDS
    public const string TITLE_REVEAL_FX_KEY = "Logo";
    public const string START_SCENE_WATER_BGM_KEY = "FromCore_StartScreen_water";

    // DIALOGS
    public const string DIALOG_MAP_CLEARED_PATH = "Dialogs/map_cleared_dialog";
}
