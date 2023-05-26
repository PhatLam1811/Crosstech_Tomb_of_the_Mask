using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public RectTransform btn_content_transform;

    private const float offset = 10.0f;

    public void OnSelect(BaseEventData eventData)
    {
        this.btn_content_transform.anchoredPosition3D += Vector3.down * offset;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.btn_content_transform.anchoredPosition3D = Vector3.zero;
    }
}
