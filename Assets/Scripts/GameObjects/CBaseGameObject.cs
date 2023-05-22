using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseGameObject : MonoBehaviour
{
    protected Vector3 movingVector;
    protected float speed;

    void Start()
    {
        this.Initialize();
    }

    protected virtual void Initialize()
    {
        this.movingVector = Vector3.zero;
        this.speed = 0.0f;
    }
}
