using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CDeviceDebugger : MonoSingleton<CDeviceDebugger>
{
    public ScrollRect scrollRect;
    public RectTransform logPos;
    public TextMeshProUGUI tmp_debug_log;

    public void Log(string logContent)
    {
        TextMeshProUGUI log = Instantiate(tmp_debug_log, this.logPos);
        log.text = logContent;
        // this.SnapTo(log.transform);
    }

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        logPos.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(logPos.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
    }
}
