using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CSpikeM : CBaseTrap
{
    public CSpikeMVisual visual;
    public BoxCollider2D spikeCollider;

    private bool _isOn = false;
    private Vector2 rayDirection;

    private const float RAY_DISTANCE = 0.3f;

    private void FixedUpdate()
    {
        if (this.ShootPlayerDetectRay())
        {
            this.OnPlayerCollided();
        }
    }

    private bool ShootPlayerDetectRay()
    {
        Vector2 rayOrigin = new Vector2(this.transform.position.x, this.transform.position.y);

        RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, this.rayDirection, RAY_DISTANCE);

        if (rayHit.collider != null)
        {
            return rayHit.collider.TryGetComponent<CPlayer>(out CPlayer player);
        }

        return false;
    }

    public void OnPlayerCollided()
    {
        if (!this._isOn)
            this.visual.PlayAnimationSpikeM();
    }

    public bool IsActive()
    {
        return this._isOn;
    }

    public void ChangeSpikeState(bool isOn)
    {
        this._isOn = isOn;
        this.spikeCollider.enabled = isOn;
    }

    public void SetUpSpikeDirection(MapTrapType id)
    {
        switch (id)
        {
            case MapTrapType.SPIKE_M_U:
                this.transform.Rotate(Vector3.forward * 90.0f);
                this.rayDirection = Vector2.down;
                break;
            case MapTrapType.SPIKE_M_L:
                this.transform.Rotate(Vector3.forward * 180f);
                this.rayDirection = Vector2.right;
                break;
            case MapTrapType.SPIKE_M_D:
                this.transform.Rotate(Vector3.forward * 270.0f);
                this.rayDirection = Vector2.up;
                break;
            case MapTrapType.SPIKE_M_R:
                this.transform.Rotate(Vector3.zero);
                this.rayDirection = Vector2.left;
                break;
        }
    }
}
