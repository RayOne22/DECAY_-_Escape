using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{   Player player;
    Background background;
    bool didGenerateCity = false;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        background = GameObject.Find("Background").GetComponent<Background>();
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * (Time.fixedDeltaTime / 20);

        if (pos.x < -75)
        {
            Destroy(gameObject);
            return;
        }
        if (!didGenerateCity)
        {
            if (pos.x < 45)
            {
                didGenerateCity = true;
                GenerateCity();
            }
        }
        transform.position = pos;
    }

    void GenerateCity()
    {
        GameObject gc = Instantiate(background.cityPrefab[Random.Range(0, 5)].gameObject);
        gc.name = gameObject.name;
        Vector2 pos;
        pos.y = 26.2f;
        pos.x = 166.6f;
        gc.transform.position = pos;
    }
}
