using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CHomeSceneUIManager : MonoSingleton<CHomeSceneUIManager>
{
    public Transform canvasPos;

    public TextMeshProUGUI tmp_player_level;
    public TextMeshProUGUI tmp_player_coins;
    public TextMeshProUGUI tmp_player_energy;
    
    public Image img_player_exp_progress_bar;

    public Button btn_story_mode_navigate;

    public CStoryModeDialog dialog_story_mode;
    public CArcadeModeDialog dialog_arcade_mode;

    public void LoadScene()
    {
        this.LoadPlayerEXP();
        this.LoadPlayerBooster();
        this.LoadStoryModeDialog();
    }

    private void LoadPlayerEXP()
    {
        int currentExp = CHomeSceneHandler.Instance.GetPlayerEXP();
        int requiredExp = CHomeSceneHandler.Instance.GetPlayerRequiredEXPToNextLevel();

        float percentExpAccquired = (currentExp * 1.0f) / requiredExp;

        this.img_player_exp_progress_bar.fillAmount = percentExpAccquired;
    }

    private void LoadPlayerBooster()
    {
        this.tmp_player_level.text = CHomeSceneHandler.Instance.GetPlayerLevel().ToString();
        this.tmp_player_coins.text = CHomeSceneHandler.Instance.GetPlayerCoins().ToString();
        this.tmp_player_energy.text = CHomeSceneHandler.Instance.GetPlayerEnergy().ToString();
    }

    private void LoadStoryModeDialog()
    {
        CHomeSceneManager.Instance.LoadNavigateDialog(this.dialog_story_mode);
    }

    public void OnBtnBuyCoinClicked()
    {

    }

    public void OnBtnRefillEnergyClicked()
    {

    }

    public void OnBtnMissionClicked()
    {

    }

    public void OnBtnSettingsClicked()
    {
        CGameManager.Instance.ShowDialog<CSettingsDialog>(
            GameDefine.DIALOG_SETTINGS_PATH, canvasPos);
    }

    public void OnBtnChestClaimClicked()
    {

    }

    public void OnBtnNavigateToShopClicked()
    {

    }

    public void OnBtnNavigateToStoryModeClicked()
    {
        CHomeSceneManager.Instance.LoadNavigateDialog(this.dialog_story_mode);
        CHomeSceneManager.Instance.UnLoadNavigateDialog(this.dialog_arcade_mode);
    }

    public void OnBtnNavigateToArcadeModeClicked()
    {
        CHomeSceneManager.Instance.LoadNavigateDialog(this.dialog_arcade_mode);
        CHomeSceneManager.Instance.UnLoadNavigateDialog(this.dialog_story_mode);
    }
}
