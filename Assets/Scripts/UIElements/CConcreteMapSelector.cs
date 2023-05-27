using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CConcreteMapSelector : MonoBehaviour
{
    public Image img_horizontal_connector;
    public Image img_vertical_up_connector;
    public Image img_vertical_down_connector;
    public TextMeshProUGUI tmp_map_id;
    public Button btn_unlocked_map;
    public Image img_locked_map;

    public int _mapId;

    private Color unlocked_color = new Color(189, 1, 253, 255);

    public void SetUpMapSelector(int mapId, bool isDownConnect, bool isLastUnlockedMap)
    {
        this._mapId = mapId;
        this.tmp_map_id.text = mapId.ToString();

        if (!CHomeSceneHandler.Instance.IsMapUnlocked(mapId))
        {
            this.btn_unlocked_map.gameObject.SetActive(false);
            this.img_locked_map.gameObject.SetActive(true);
        }
        else
        {
            this.btn_unlocked_map.gameObject.SetActive(true);
            this.img_locked_map.gameObject.SetActive(false);
        }

        if (mapId % 2 == 0)
        {
            this.img_vertical_up_connector.gameObject.SetActive(false);
            this.img_vertical_down_connector.gameObject.SetActive(false);
            if (isLastUnlockedMap)
            {
                this.img_horizontal_connector.color = this.unlocked_color;
            }
        }
        else
        {
            this.img_horizontal_connector.gameObject.SetActive(false);

            if (isDownConnect)
            {
                this.img_vertical_up_connector.gameObject.SetActive(false);
                if (isLastUnlockedMap)
                {
                    this.img_vertical_down_connector.color = this.unlocked_color;
                }
            }
            else
            {
                this.img_vertical_down_connector.gameObject.SetActive(false);
                if (isLastUnlockedMap)
                {
                    this.img_vertical_up_connector.color = this.unlocked_color;
                }
            }
        }
    }

    public void OnClicked()
    {
        CHomeSceneHandler.Instance.PlayMap(this._mapId);
    }
}
