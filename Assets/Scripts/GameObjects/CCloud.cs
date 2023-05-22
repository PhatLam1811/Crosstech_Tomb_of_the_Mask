using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCloud : CBaseGameObject
{
    private static float minSpeed = 0.5f;
    private static float maxSpeed = 1.0f;

    private static float outOfRightEdgeViewportPosX = 1.3f;
    private static float outOfLeftEdgeViewportPosX = -0.3f;

    private static float minRePositionViewportPosY = 0.6f;
    private static float maxRePositionViewportPosY = 0.9f;

    void Update()
    {
        this.Move();        
    }

    protected override void Initialize()
    {
        this.movingVector = Vector3.right;
        this.speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Move()
    {
        Vector3 currentViewportPos = Utils.WorldToViewportPos(Camera.main, this.transform.position);

        if (currentViewportPos.x >= outOfRightEdgeViewportPosX)
        {
            this.transform.position = this.RandomizeRePosition();
        }

        this.transform.position += this.movingVector * this.speed * Time.deltaTime;
    }

    private Vector3 RandomizeRePosition()
    {
        float viewportRePositionPosX = outOfLeftEdgeViewportPosX;
        float viewportRePositionPosY = Random.Range(minRePositionViewportPosY, maxRePositionViewportPosY);

        Vector3 viewportRePositionPos = new Vector3(viewportRePositionPosX, viewportRePositionPosY);

        Vector3 worldRePositionPos = Utils.ViewportToWorldPos(Camera.main, viewportRePositionPos);

        worldRePositionPos.z = this.transform.position.z;

        return worldRePositionPos;
    }
}
