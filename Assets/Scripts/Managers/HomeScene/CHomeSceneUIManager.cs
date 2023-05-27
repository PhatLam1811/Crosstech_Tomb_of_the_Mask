using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CHomeSceneUIManager : MonoSingleton<CHomeSceneUIManager>
{
    public TextMeshProUGUI tmp_player_level;
    public TextMeshProUGUI tmp_player_coins;
    public TextMeshProUGUI tmp_player_energy;
    public Image img_player_exp_progress_bar;

    public void LoadScene()
    {
        int currentExp = CHomeSceneHandler.Instance.GetPlayerEXP();
        int requiredExp = CHomeSceneHandler.Instance.GetPlayerRequiredEXPToNextLevel();

        float percentExpAccquired = (currentExp * 1.0f) / requiredExp;

        this.img_player_exp_progress_bar.fillAmount = percentExpAccquired;

        this.tmp_player_level.text = CHomeSceneHandler.Instance.GetPlayerLevel().ToString();
        this.tmp_player_coins.text = CHomeSceneHandler.Instance.GetPlayerCoins().ToString();
        this.tmp_player_energy.text = CHomeSceneHandler.Instance.GetPlayerEnergy().ToString();
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

    }

    public void OnBtnChestClaimClicked()
    {

    }

    public void OnBtnNavigateToShopClicked()
    {

    }

    public void OnBtnNavigateToMapClicked()
    {

    }

    public void OnBtnNavigateToArcadeModeClicked()
    {

    }
}
