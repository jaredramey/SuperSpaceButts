using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy_HealthManager : MonoBehaviour
{
    //Set enemy health
    public int health = 0;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Box collider for collision with player.
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            health--;
        }

    }

    //Circle collider for player attacking enemy.
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            Player_HealthManager.Instance.OnPlayerTakeDamage.Invoke(); 
        }
    }
}
