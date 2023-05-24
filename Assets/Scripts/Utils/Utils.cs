using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 WorldToViewportPos(Camera camera, Vector3 worldPos)
    {
        return camera.WorldToViewportPoint(worldPos);
    }

    public static Vector3 ViewportToWorldPos(Camera camera, Vector3 viewportPos)
    {
        return camera.ViewportToWorldPoint(viewportPos);
    }

    public static Vector3 WorldToScreenPos(Camera camera, Vector3 worldPos)
    {
        return camera.WorldToScreenPoint(worldPos);
    }

    public static Vector3 ScreenToWorldPos(Camera camera, Vector3 screenPos)
    {
        return camera.ScreenToWorldPoint(screenPos);
    }

    public static Vector3 ViewportToScreenPos(Camera camera, Vector3 viewportPos)
    {
        return camera.ViewportToScreenPoint(viewportPos);
    }

    public static Vector3 ScreenToViewportPos(Camera camera, Vector3 screenPos)
    {
        return camera.ScreenToViewportPoint(screenPos);
    }
}
