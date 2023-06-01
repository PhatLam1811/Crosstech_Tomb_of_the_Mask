using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CStoryModeDialog : CBaseDialog
{
    public GameObject prefab_concrete_map_selector;

    public Transform panel_container_transform_row_1;
    public Transform panel_container_transform_row_2;

    private bool _isMapLoaded = false;

    private new void OnEnable() { }

    public override void OnShow(object data = null, UnityAction callback = null)
    {
        base.OnShow(data, callback);
        this.LoadMapSelectorsList();
    }

    public override void OnHide()
    {
        this.OnCompleteHide();
    }

    private void LoadMapSelectorsList()
    {
        if (this._isMapLoaded) return;

        this._isMapLoaded = true;

        int counter = 2;
        int mapsCount = CHomeSceneHandler.Instance.GetMapsCount();
        Transform currentRow = this.panel_container_transform_row_1;

        bool isDownConnectorNext = true;

        for (int i = 1; i <= mapsCount; i++)
        {
            if (i % 2 == 0)
            {
                isDownConnectorNext = !isDownConnectorNext;
            }

            this.InstantiateNewMapSelector(i, currentRow, isDownConnectorNext);

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

    private void InstantiateNewMapSelector(int mapId, Transform pos, bool isDownConnectorNext)
    {
        GameObject newConcreteMapSelector = Instantiate(this.prefab_concrete_map_selector, pos);

        if (newConcreteMapSelector.TryGetComponent(out CConcreteMapSelector script))
        {
            script.SetUpMapSelector(mapId, isDownConnectorNext);
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
