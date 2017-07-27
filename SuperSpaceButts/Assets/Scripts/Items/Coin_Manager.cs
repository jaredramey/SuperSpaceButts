using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is to handle all coin related behavior. Points can be changed as needed in inspector.
public class Coin_Manager : MonoBehaviour
{
    [SerializeField]
    private int pointsToAdd = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        //If player enters collider
        if(col.name == "Player")
        {
            //Add points
            Player_ScoreManager.Instance.AddPoints(pointsToAdd);
            //Destroy game object
            Destroy(gameObject);
        }
    }
}
