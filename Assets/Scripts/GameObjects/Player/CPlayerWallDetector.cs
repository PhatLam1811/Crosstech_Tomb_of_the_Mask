using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CPlayerWallDetector : MonoBehaviour
{
    public UnityAction<Vector3> onCollided;
    public BoxCollider2D _collider;
    public CPlayer _player;

    private const int mapLayer = 6;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("On Collision Enter");
    //    this.OnWallCollided(true);
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (!this._isCollided)
    //    {
    //        Debug.Log("On Collision Enter");
    //        this.OnWallCollided(false);
    //    }
    //}

    public void ShootRaycast2D()
    {
        //Vector3 originPos = this.transform.position;
        //Vector3 playerMovingVector = this._player.GetMovingVector();
        //float playerSpeed = this._player.GetSpeed();

        //Vector2 rayOrigin = new Vector2(originPos.x, originPos.y);
        //Vector2 rayDirection = new Vector2(playerMovingVector.x, playerMovingVector.y);
        //float rayDistance = 15.0f * Time.deltaTime;
        //int detectLayerMask = 1 << mapLayer;

        //RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, detectLayerMask);
        //Debug.DrawLine(originPos, originPos + playerMovingVector * rayDistance, Color.red);

        //if (rayHit.collider != null)
        //{
        //    Vector3 collidedPoint = new Vector3(rayHit.point.x, rayHit.point.y, 0);
        //    Vector3 distanceToWall = collidedPoint - originPos;
        //    this.InvokeOnWallCollidedCallback(distanceToWall);
        //}
    }

    public void AssignOnWallCollidedCallback(UnityAction<Vector3> callback)
    {
        this.onCollided -= callback;
        this.onCollided += callback;
    }

    public void UnAssignOnWallCollidedCallback(UnityAction<Vector3> callback)
    {
        this.onCollided -= callback;
    }

    public void InvokeOnWallCollidedCallback(Vector3 distanceToWall)
    {
        this.onCollided?.Invoke(distanceToWall);
    }
}
