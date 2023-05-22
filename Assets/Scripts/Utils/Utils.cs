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
}
