using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseCollectableObject : CBaseGameObject
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public virtual void OnCollectedByPlayer()
    {
        Destroy(this.gameObject);
    }
}
