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

    public void SetUpMapSelector(int mapId, bool isDownConnect)
    {
        this.tmp_map_id.text = mapId.ToString();

        if (mapId % 2 == 0)
        {
            this.img_vertical_up_connector.gameObject.SetActive(false);
            this.img_vertical_down_connector.gameObject.SetActive(false);
        }
        else
        {
            this.img_horizontal_connector.gameObject.SetActive(false);

            if (isDownConnect)
            {
                this.img_vertical_up_connector.gameObject.SetActive(false);
            }
            else
            {
                this.img_vertical_down_connector.gameObject.SetActive(false);
            }
        }
    }
}
