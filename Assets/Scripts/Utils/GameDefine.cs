using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine
{
    // SCENES
    public const int LOADING_SCENE_ID = 0;
    public const int HOME_SCENE_ID = 1;
    public const int PLAY_SCENE_ID = 2;

    // DATA
    public const string GAME_DATA = "GAME_DATA";

    // CONFIGS
    public const string PLAYER_CONFIGS_FILE_PATH = "Configs/PlayerConfigs";
    public const string MAP_CONFIGS_FILE_PATH = "Configs/MapConfigs";
    public const string SOUND_CONFIGS_FILE_PATH = "Configs/SoundConfigs";

    // ANIMATIONS
    public const string TITLE_REVEAL_ANIM = "title_reveal";
    public const string NOTIFY_ANIM = "notify";
    public const string STAR_GETS_ANIM = "star_get";
    public const string PURCHASE_FAILED_ANIM = "purchase_failed";
    public const string FADE_ON_ANIM = "fade_on";
    public const string FADE_OFF_ANIM = "fade_off";

    // TILES
    public const int DOT_TILE_ID = 0;
    public const int STAR_TILE_ID = 1;
    public const int COIN_TILE_ID = 2;
    public const int EXIT_TILE_ID = 3;

    // SOUNDS
    public const string TITLE_REVEAL_FX_KEY = "Logo";
    public const string STAR_COLLECTED_1_FX_KEY = "1_star";
    public const string STAR_COLLECTED_2_FX_KEY = "2_star";
    public const string STAR_COLLECTED_3_FX_KEY = "3_star";
    public const string DOTS_COUNT_FX_KEY = "Score_count";
    public const string BONUS_CLAIMED_FX_KEY = "Power_up_off";
    public const string BUTTON_CLICK_FX_KEY = "Button_2";
    public const string TAP_TO_PLAY_FX_KEY = "Fade_zoom_1";
    public const string DOT_PICK_UP_FX_KEY = "Coin_1";
    public const string COIN_PICK_UP_FX_KEY = "Coin_3";
    public const string STAR_PICK_UP_FX_KEY = "Star_pick_up";
    public const string PLAYER_DIE_FX_KEY = "Death";
    public const string PLAYER_DEFAULT_MASK_FX_KEY = "MaskDefault";
    public const string PLAYER_LANDING_FX_KEY = "Landing";
    public const string PLAYER_WIN_FX_KEY = "Win";
    public const string GAME_START_FX_KEY = "Start";
    public const string SHIELD_ACTIVATE_FX_KEY = "Shield";
    public const string SPIKE_M_ON_FX_KEY = "Spikesinwalls_attack";
    public const string SPIKE_M_OFF_FX_KEY = "Spikesinwalls_on_off";
    public const string RESCUE_NO_FX_KEY = "Rescue_no";
    public const string RESCUE_YES_FX_KEY = "Rescue_yes";
    public const string ICE_BREAK_FX_KEY = "Ice";
    public const string LOADING_SCENE_WATER_BGM_KEY = "FromCore_StartScreen_water";
    public const string HOME_SCENE_BGM_KEY = "Music_music";
    public const string GAMEPLAY_BGM_KEY = "Story_music";

    // DIALOGS
    public const string DIALOG_MAP_CLEARED_PATH = "Dialogs/map_cleared_dialog";
    public const string DIALOG_REVIVE_PATH = "Dialogs/revive_dialog";
    public const string DIALOG_SETTINGS_PATH = "Dialogs/settings_dialog";
    public const string DIALOG_COMING_SOON_PATH = "Dialogs/coming_soon_dialog";
    public const string DIALOG_PAUSE_PATH = "Dialogs/pause_dialog";

    // LAYERS
    public const int MAP_LAYER = 6;
    public const int SPIKE_LAYER = 7;
}
