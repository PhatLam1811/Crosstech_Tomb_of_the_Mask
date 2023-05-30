using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CConcreteMapSelector : MonoBehaviour
{
    public Image img_horizontal_connector;
    public Image img_vertical_up_connector;
    public Image img_vertical_down_connector;
    public Image img_locked_horizontal_connector;
    public Image img_locked_vertical_up_connector;
    public Image img_locked_vertical_down_connector;
    public TextMeshProUGUI tmp_map_id;
    public Button btn_unlocked_map;
    public Image img_locked_map;
    public CGameplayStarsHolder star_holders;

    public int _mapId;

    public void SetUpMapSelector(int mapId, bool isDownConnect)
    {
        this._mapId = mapId;
        this.tmp_map_id.text = mapId.ToString();

        bool isUnlocked = CHomeSceneHandler.Instance.IsMapUnlocked(this._mapId);
        bool isLastUnlocked = this.IsLastUnlockedMap();

        this.LoadMapState(isUnlocked);

        bool isLastMap = CHomeSceneHandler.Instance.IsLastMap(this._mapId);

        if (!isLastMap)
        {
            if (!isUnlocked || isLastUnlocked)
                this.LoadLockedConnectors(isDownConnect);
            else
                this.LoadUnlockedConnectors(isDownConnect);
        }

        if (isUnlocked)
        {
            this.LoadMapStars();
        }
    }

    private void LoadMapState(bool isUnlocked)
    {
        this.btn_unlocked_map.gameObject.SetActive(isUnlocked);
        this.img_locked_map.gameObject.SetActive(!isUnlocked);
    }

    private void LoadUnlockedConnectors(bool isDownConnect)
    {
        if (this._mapId % 2 == 0)
        {
            this.img_horizontal_connector.gameObject.SetActive(true);
            return;
        }

        this.img_vertical_down_connector.gameObject.SetActive(isDownConnect);
        this.img_vertical_up_connector.gameObject.SetActive(!isDownConnect);
    }

    private void LoadLockedConnectors(bool isDownConnect)
    {
        if (this._mapId % 2 == 0)
        {
            this.img_locked_horizontal_connector.gameObject.SetActive(true);
            return;
        }

        this.img_locked_vertical_down_connector.gameObject.SetActive(isDownConnect);
        this.img_locked_vertical_up_connector.gameObject.SetActive(!isDownConnect);
    }

    private void LoadMapStars()
    {
        long starCount = CHomeSceneHandler.Instance.GetMapStars(this._mapId);

        for (int i = 1; i <= starCount; i++)
        {
            this.star_holders.OnStarCollected(i);
        }
    }

    private bool IsLastUnlockedMap()
    {
        return this._mapId == CHomeSceneHandler.Instance.GetLastUnlockedMap();
    }

    public void OnClicked()
    {
        CHomeSceneHandler.Instance.PlayMap(this._mapId);
    }
}
