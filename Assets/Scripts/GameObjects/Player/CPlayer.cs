using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CBaseGameObject
{
    private Vector3 movingVector;
    private float speed;

    private Queue<PlayerMoves> movesOnStandBy;

    private bool _isOnPlatform;

    private void Start()
    {
        this.Initialize();
    }

    private void Update()
    {
        // only move in another direction if the player has collided with a wall
        if (this._isOnPlatform)
        {
            this.PrepareForNextMove();
        }

        this.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Is Collided!");

        if (!this._isOnPlatform)
        {
            this.OnHittingWalls();
        }
    }

    private void Initialize()
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
                    // this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse); break;
                case PlayerMoves.Left:
                    this.movingVector = Vector3.left; break;
                    // this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f, ForceMode2D.Impulse); break;
                case PlayerMoves.Down:
                    this.movingVector = Vector3.down; break;
                    // this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5f, ForceMode2D.Impulse); break;
                case PlayerMoves.Right:
                    this.movingVector = Vector3.right; break;
                    // this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5f, ForceMode2D.Impulse); break;
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
        Debug.Log(this.transform.position);
        this.transform.position += this.movingVector * -0.037f;
        this.movingVector = Vector3.zero;
        this._isOnPlatform = true;
    }

    public void StartGame()
    {
        this.movingVector = Vector3.zero;
        this.speed = 6.0f; // this will be configurate outside later on

        this.movesOnStandBy = new Queue<PlayerMoves>();

        this._isOnPlatform = true;
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
