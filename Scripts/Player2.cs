using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] Player player;


    public void GameOver()
    {
        player.isKnockOut = true;
    }
}
