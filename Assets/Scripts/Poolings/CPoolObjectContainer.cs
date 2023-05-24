using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CPoolObjectContainer
{
    public int _id;
    public CPoolObjectHandler poolHandler;

    public void ReturnObject(GameObject t)
    {
        if (poolHandler == null) return;
        poolHandler.ReturnObject(t);
    }
    public T RequestObject<T>() where T : MonoBehaviour
    {
        if (poolHandler == null) return null;
        return poolHandler.RequestObject() as T;
    }
    public GameObject RequestObject()
    {
        if (poolHandler == null) return null;
        return poolHandler.RequestObject();
    }
    public void Init()
    {
        if (poolHandler == null) return;
        poolHandler.InitStack();
    }
}
