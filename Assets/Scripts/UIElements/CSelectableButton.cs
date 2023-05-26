using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CSelectableButton : CButton, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        this.btn_content_transform.anchoredPosition3D += Vector3.down * offset;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.btn_content_transform.anchoredPosition3D = Vector3.zero;
    }
}
