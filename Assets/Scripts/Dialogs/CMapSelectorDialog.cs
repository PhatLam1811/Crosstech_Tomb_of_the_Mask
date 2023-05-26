using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CMapSelectorDialog : CBaseDialog
{
    public GameObject prefab_concrete_map_selector;

    public Transform panel_container_transform_row_1;
    public Transform panel_container_transform_row_2;

    private bool isDownConnectorNext = true;

    private new void OnEnable() 
    {
        this.OnShow();
    }

    public override void OnShow(object data = null, UnityAction callback = null)
    {
        base.OnShow(data, callback);

        int counter = 2;
        Transform currentRow = this.panel_container_transform_row_1;

        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 == 0)
            {
                this.isDownConnectorNext = !this.isDownConnectorNext;
            }

            this.InstantiateNewMapSelector(i, currentRow);

            if (counter == 2)
            {
                counter = 1;
                this.SwitchInstantiateRow(ref currentRow);
            }
            else
            {
                counter++;
            }
        }
    }

    private void InstantiateNewMapSelector(int mapId, Transform pos)
    {
        GameObject newConcreteMapSelector = Instantiate(this.prefab_concrete_map_selector, pos);

        if (newConcreteMapSelector.TryGetComponent(out CConcreteMapSelector script))
        {
            script.SetUpMapSelector(mapId, this.isDownConnectorNext);
        }
    }

    private void SwitchInstantiateRow(ref Transform currentRow)
    {
        if (currentRow != this.panel_container_transform_row_1)
        {
            currentRow = this.panel_container_transform_row_1;
        }
        else
        {
            currentRow = this.panel_container_transform_row_2;
        }
    }
}
