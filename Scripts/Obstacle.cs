using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;
    public Animator componentAnimator;


    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -20)
        {
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
