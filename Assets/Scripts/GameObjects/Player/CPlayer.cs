using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CPlayer : CBaseGameObject
{
    private Queue<PlayerMoves> movesOnStandBy;

    private bool _isOnPlatform;

    private void Update()
    {
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
            CGamePlayManager.Instance.OnPlayerHitDotGame(dotGame); return;
        }

        if (collision.TryGetComponent<CStar>(out CStar star))
        {
            CGamePlayManager.Instance.OnPlayerHitStar(star); return;
        }

        if (collision.TryGetComponent<CCoin>(out CCoin coin))
        {
            CGamePlayManager.Instance.OnPlayerHitCoin(coin); return;
        }

        Debug.Log("Is Triggered!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Is Collided!");

        if (!this._isOnPlatform)
        {
            this.OnHittingWalls();
        }
    }

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
        }
    }

    private void Move()
    {
        this.transform.position += this.movingVector * this.speed * Time.deltaTime;
    }

    private void OnHittingWalls()
    {
        // this.transform.position += this.movingVector * -0.037f;
        this.movingVector = Vector3.zero;
        this._isOnPlatform = true;
    }

    public void StartGame()
    {
        this.speed = 6.0f; // this will be configurate outside later on
    }

    public void RegisterNextMove(PlayerMoves nextMove)
    {
        this.movesOnStandBy.Enqueue(nextMove);
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
