using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseCollectableObject : CBaseGameObject
{
    public virtual void OnCollectedByPlayer()
    {
        Destroy(this.gameObject);
    }
}
