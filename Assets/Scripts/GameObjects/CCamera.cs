using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CCamera : CBaseGameObject
{
    private Transform _playerTransform;

    private void Start()
    {
        this._playerTransform = CGameplayManager.Instance.GetPlayer().transform;
    }

    private void Update()
    {
        Vector3 newPos = this._playerTransform.position;
        
        // only follow player on X & Y axes
        newPos.z = this.transform.position.z;

        // camera follow player
        this.transform.DOMove(newPos, 0.5f);
    }
}
