using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CButton : MonoBehaviour, IPointerClickHandler
{
    public RectTransform btn_content_transform;

    protected const float offset = 10.0f;

    public void OnPointerClick(PointerEventData eventData)
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
    }
}
