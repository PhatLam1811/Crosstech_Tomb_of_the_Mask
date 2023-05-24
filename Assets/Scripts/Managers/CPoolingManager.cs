using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPoolingManager : MonoSingleton<CPoolingManager>
{
    [SerializeField] private List<CPoolObjectContainer> _listPoolObjects;
    public Dictionary<int, CPoolObjectContainer> _dicObjectHandlers;

    private void Start()
    {
        InitPooling();
    }

    public void InitPooling()
    {
        _dicObjectHandlers = new Dictionary<int, CPoolObjectContainer>();
        foreach (CPoolObjectContainer p in this._listPoolObjects)
        {
            if (!_dicObjectHandlers.ContainsKey(p._id))
                _dicObjectHandlers.Add(p._id, p);

            p.Init();
        }
    }

    private CPoolObjectHandler GetPoolHanler(int id)
    {
        if (_dicObjectHandlers == null) return null;
        if (_dicObjectHandlers.ContainsKey(id))
            return _dicObjectHandlers[id].poolHandler;

        return null;
    }

    public void ReturnObject(CPoolObjectHandler pool, GameObject o)
    {
        if (pool != null)
            pool.ReturnObject(o);
    }

    public void ReturnObject(int id, GameObject o)
    {
        CPoolObjectHandler pool = GetPoolHanler(id);
        if (pool != null)
            pool.ReturnObject(o);
    }

    public T RequestObject<T>(int id) where T : MonoBehaviour
    {
        CPoolObjectHandler pool = GetPoolHanler(id);
        if (pool != null)
            return pool.RequestObject() as T;

        return null;
    }

    public GameObject RequestObject(int id)
    {
        CPoolObjectHandler pool = GetPoolHanler(id);
        if (pool != null)
            return pool.RequestObject();

        return null;
    }
}



