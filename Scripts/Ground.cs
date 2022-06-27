using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Obstacle obstacle;
    BoxCollider2D groundCollider;

    bool didGenerateGround = false;
    public float groundHeight;
    [SerializeField] float groundRight;
    [SerializeField] float screenRight;


    void Awake()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (groundCollider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (groundCollider.size.x / 2);

        if (groundRight < -20)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < 66)
            {
                didGenerateGround = true;
                GenerateGround();
            }
        }

        transform.position = pos;
    }

    void GenerateGround()
    {
        GameObject go = Instantiate(gameObject);
        go.name = gameObject.name;
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        float h1 = player.jumpVelocity * player.maxHoldJumpTime;
        float t = player.jumpVelocity / -player.gravity;
        float h2 = player.jumpVelocity * t + (0.2f * (player.gravity * (t + t)));
        float maxJumpHeight = h1 + h2;
        float maxY = maxJumpHeight * 0.7f;
        maxY += groundHeight - 9.5f;
        float minY = 2.5f;
        float actualY = Random.Range(minY, maxY);
        pos.y = actualY - goCollider.size.y / 2 + 9.5f;
        if (pos.y > 7f)
        {
            pos.y = Random.Range(minY - goCollider.size.y / 2 + 9.5f, 7f);
        }

        float t1 = t + player.maxHoldJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * (player.distance * 0.017f);
        maxX *= 0.7f;
        maxX += groundRight;
        float minX = screenRight + 14;
        if (maxX <= minX)
        {
            maxX = screenRight + 15;
        }
        float actualX = Random.Range(minX, maxX);
        pos.x = actualX + goCollider.size.x / 2;
        if (pos.x > 190)
        {
            pos.x = Random.Range(minX + goCollider.size.x / 2, 190);
        }
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);

        int obstacleNum;
        if (player.distance <= 500)
        {
            obstacleNum = 0;
        }
        else
        {
            obstacleNum = Random.Range(0, 2);
        }

        for (int i = 0; i < obstacleNum; i++)
        {
            GameObject obstacle = Instantiate(this.obstacle.gameObject);
            obstacle.name = this.obstacle.name;
            float y = goGround.groundHeight + 0.73f;
            float halfWidth = goCollider.size.x / 2 - 40;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = Random.Range(left, right);
            Vector2 obstaclePos = new Vector2(x, y);
            obstacle.transform.position = obstaclePos;
        }
    }
}