using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPoolObjectHandler : MonoBehaviour
{
    public GameObject prefab;
    public Stack<GameObject> stackObjs;

    private Transform _transform;
    public Transform CachedTransform
    {
        get
        {
            if (this._transform == null)
                this._transform = this.transform;
            return this._transform;
        }
    }

    public int _amountObjectSetup = 1000;
    public Coroutine _coroutineAddObjs;

    private void OnDisable()
    {
        StopCoroutine(_coroutineAddObjs);
    }

    public void InitStack()
    {
        this.stackObjs = new Stack<GameObject>();
        _coroutineAddObjs = StartCoroutine(coroutineAddObject());
    }

    IEnumerator coroutineAddObject()
    {
        int counter = 0;
        for (int i = 0; i < _amountObjectSetup; i++)
        {
            counter += 1;
            AddOneObject();
            if(counter >= 10)
            {
                counter = 0;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void AddOneObject()
    {
        GameObject obj = Instantiate<GameObject>(this.prefab, this.transform);
        ReturnObject(obj);
    }

    public GameObject RequestObject()
    {
        if (this.stackObjs == null)
        {
            InitStack();
        }
        if (this.stackObjs.Count == 0) AddOneObject();
        GameObject obj = this.stackObjs.Pop();
        obj?.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj == null)
            return;
        if (this.stackObjs == null)
            this.stackObjs = new Stack<GameObject>();
        obj.gameObject.SetActive(false);
        this.stackObjs.Push(obj);

        obj.transform.SetParent(this.CachedTransform);
    }
}