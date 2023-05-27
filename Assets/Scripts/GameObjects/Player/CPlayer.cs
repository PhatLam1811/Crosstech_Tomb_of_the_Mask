using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum PlayerMoves
{
    Up,
    Left,
    Down,
    Right
}

public class CPlayer : CBaseGameObject
{
    public CPlayerVisual _visual;

    public Transform _raycastPos1;
    public Transform _raycastPos2;

    private int _currentAngle;

    private Queue<PlayerMoves> movesOnStandBy;
    
    private bool _isMoving;

    private const int MAP_LAYER = 6;

    private const float RAYCAST_1_DISTANCE = 15.0f;
    private const float RAYCAST_2_DISTANCE = 25.0f;

    private const float RAYCAST_2_PUSH_OFFSET = -0.25f;

    private void FixedUpdate()
    {
        // only move in another direction if the player has collided with a wall
        if (!this._isMoving)
            this.PrepareForNextMove();
        else
            this.AttemptToMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CDotGame>(out CDotGame dotGame))
        {
            CGameplayManager.Instance.OnPlayerHitDotGame(dotGame); return;
        }

        if (collision.TryGetComponent<CStar>(out CStar star))
        {
            CGameplayManager.Instance.OnPlayerHitStar(star); return;
        }

        if (collision.TryGetComponent<CCoin>(out CCoin coin))
        {
            CGameplayManager.Instance.OnPlayerHitCoin(coin); return;
        }

        if (collision.TryGetComponent<CExit>(out CExit exit))
        {
            CGameplayManager.Instance.OnPlayerReachExit(); return;
        }
    }

    protected override void Initialize()
    {
        this.movingVector = Vector3.zero;
        this.speed = 15.0f; // this will be configurate outside later on

        this.movesOnStandBy = new Queue<PlayerMoves>();

        this._isMoving = false;

        this._currentAngle = 0;
    }

    public void StartGame()
    {
        this.speed = 3.0f; // this will be configurate outside later on
        StartCoroutine(this.PlayStartGameAnimation());
    }

    private IEnumerator PlayStartGameAnimation()
    {
        this._visual.PlayStartAnimation();

        yield return new WaitForSeconds(2.0f);

        this._visual.PlayAnimation(CPlayerVisual.PLAYER_IDLE_ANIM);
    }

    public void RegisterNextMove(PlayerMoves nextMove)
    {
        this.movesOnStandBy.Enqueue(nextMove);
    }

    private void PrepareForNextMove()
    {
        if (this.movesOnStandBy.Count > 0)
        {
            PlayerMoves nextMove = this.movesOnStandBy.Dequeue();

            switch (nextMove)
            {
                case PlayerMoves.Up:
                    this.movingVector = Vector3.up;
                    this.RotateZOnMoving(0);
                    break;
                case PlayerMoves.Left:
                    this.movingVector = Vector3.left;
                    this.RotateZOnMoving(90);
                    break;
                case PlayerMoves.Down:
                    this.movingVector = Vector3.down;
                    this.RotateZOnMoving(180);
                    break;
                case PlayerMoves.Right:
                    this.movingVector = Vector3.right;
                    this.RotateZOnMoving(270);
                    break;
                default: 
                    break;
            }

            this._isMoving = true;
        }
    }

    private void AttemptToMove()
    {
        Vector3 movingDistance = Vector3.zero;
        
        bool isWallCollided = this.CheckWallCollision(out movingDistance);
        
        this.Move(movingDistance);

        if (!isWallCollided)
            this._visual.PlayAnimation(CPlayerVisual.PLAYER_JUMP_ANIM);
        else
            this.OnLanding();
    }

    private bool CheckWallCollision(out Vector3 movingDistance)
    {
        Vector3 movingDistance1;
        Vector3 movingDistance2;

        bool isHitOnRay1 = this.ShootRaycast2D(this._raycastPos1, out movingDistance1);
        bool isHitOnRay2 = this.ShootRaycast2D(this._raycastPos2, out movingDistance2);

        if (isHitOnRay2)
        {
            movingDistance = movingDistance2 + this.movingVector * RAYCAST_2_PUSH_OFFSET;
            return isHitOnRay2;
        }
        else
        {
            movingDistance = movingDistance1;
            return isHitOnRay1;
        }
    }

    private void Move(Vector3 movingDistance)
    {
        this._isMoving = true;
        this.transform.position += movingDistance;
    }

    private bool ShootRaycast2D(Transform raycastPos, out Vector3 movingDistance)
    {
        Vector2 rayOrigin = new Vector2(raycastPos.position.x, raycastPos.position.y);
        Vector2 rayDirection = new Vector2(this.movingVector.x, this.movingVector.y);
        float rayDistance = raycastPos == this._raycastPos1 ? 
            RAYCAST_1_DISTANCE * Time.deltaTime : 
            RAYCAST_2_DISTANCE * Time.deltaTime;
        int detectLayerMask = 1 << MAP_LAYER;

        RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, detectLayerMask);
        
        if (raycastPos == this._raycastPos1)
        {
            Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * rayDistance, Color.red);
        }
        else
        {
            Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * rayDistance, Color.blue);
        }


        if (rayHit.collider != null)
        {
            Vector3 collidedPoint = new Vector3(rayHit.point.x, rayHit.point.y, 0.0f);
            movingDistance = collidedPoint - new Vector3(rayOrigin.x, rayOrigin.y, 0.0f);
            movingDistance = movingDistance.magnitude >= 0.05f ? movingDistance : Vector3.zero;
            return true;
        }

        movingDistance = this.movingVector * this.speed * Time.deltaTime;
        return false;
    }

    private void OnLanding()
    {
        this.RotateZOnLanding();

        this._visual.PlayAnimation(CPlayerVisual.PLAYER_IDLE_ANIM);

        this.movingVector = Vector3.zero;

        this._isMoving = false;
    }

    private void RotateZOnMoving(int angle)
    {
        this._currentAngle = angle;
        this.transform.DORotate(Vector3.forward * angle, 0f);
    }

    private void RotateZOnLanding()
    {
        this._currentAngle = (this._currentAngle + 180) % 360;
        this.transform.DORotate(Vector3.forward * this._currentAngle, 0f);
    }
}
