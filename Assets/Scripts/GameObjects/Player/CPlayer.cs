using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CPlayer : CBaseGameObject
{
    /// <summary>
    /// 4 collider ở 4 phía của player, nhưng theo game gốc thì chỉ cần 1 cái ở dưới chân thôi vì player có rotation nữa. Cái này optimize sau
    /// </summary>
    public CPlayerCollider[] _colliders;
    bool isMoving;

    private Queue<PlayerMoves> movesOnStandBy;

    private bool _isOnPlatform;
    private void LateUpdate()
    {
        if (!isMoving)
            return;

        // only move in another direction if the player has collided with a wall
        if (this._isOnPlatform)
        {
            this.PrepareForNextMove();
        }

        this.Move();
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

        Debug.Log("Is Triggered!");
    }

    /// <summary>
    /// Dời logic bắt collision vào các collider con
    /// </summary>
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionEnter2D!");

    //    if (!this._isOnPlatform)
    //    {
    //        this.OnHittingWalls();
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionStay2D!");

    //    if (!this._isOnPlatform)
    //    {
    //        this.OnHittingWalls();
    //    }
    //}
    protected override void Initialize()
    {
        this.movingVector = Vector3.zero;
        this.speed = 1.0f; // this will be configurate outside later on

        this.movesOnStandBy = new Queue<PlayerMoves>();

        this._isOnPlatform = true;
    }

    private void PrepareForNextMove()
    {
        if (this.movesOnStandBy.Count > 0)
        {
            PlayerMoves nextMove = this.movesOnStandBy.Dequeue();

            switch (nextMove)
            {
                case PlayerMoves.Up:
                    this.movingVector = Vector3.up; break;
                case PlayerMoves.Left:
                    this.movingVector = Vector3.left; break;
                case PlayerMoves.Down:
                    this.movingVector = Vector3.down; break;
                case PlayerMoves.Right:
                    this.movingVector = Vector3.right; break;
                default: 
                    break;
            }

            this._isOnPlatform = false;

            ///Chỉ bật collider ở hướng user đang di chuyển, các collider khác tắt đi để không quẹt cạ vào tường
            for (int i = 0; i < _colliders.Length; i++)
            {
                if (i == (int)nextMove)
                    this._colliders[i].ReadyForCatchCollide();
                else
                    this._colliders[i].TurnCollider(false);
            }

            isMoving = true;

            //Move();
        }
    }

    private void Move()
    {
        this.transform.position += this.movingVector * this.speed * Time.deltaTime;
    }

    /// <summary>
    /// Chạm tường:
    /// Không di chuyển nửa
    /// Snap lại vị trí của player item cho đúng vào cell
    /// </summary>
    private void OnHittingWalls()
    {
        this.movingVector = Vector3.zero;
        this._isOnPlatform = true;
        this.transform.position += this.movingVector * -0.037f;
        isMoving = false;
    }
    /// <summary>
    /// Collider invoke khi nó va chạm wall => tắt nó đi và xử lý chạm tường
    /// </summary>
    /// <param name="collider"></param>
    private void OnColliderReceive(CPlayerCollider collider)
    {
        OnHittingWalls();
        collider.TurnCollider(false);
    }

    public void StartGame()
    {
        this.speed = 6.0f; // this will be configurate outside later on


        foreach (CPlayerCollider collider in this._colliders)
        {
            collider.onCollider -= OnColliderReceive;
            collider.onCollider += OnColliderReceive;

            collider.TurnCollider( false);
        }

    }

    public void RegisterNextMove(PlayerMoves nextMove)
    {
        this.movesOnStandBy.Enqueue(nextMove);
        PrepareForNextMove();
        Debug.Log(this.movesOnStandBy.Count);
    }
}

public enum PlayerMoves
{
    Up, 
    Left, 
    Down, 
    Right
}
