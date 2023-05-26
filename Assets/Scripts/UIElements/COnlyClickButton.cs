using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class COnlyClickButton : CButton, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        this.btn_content_transform.anchoredPosition3D += Vector3.down * offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.btn_content_transform.anchoredPosition3D = Vector3.zero;
    }
}
