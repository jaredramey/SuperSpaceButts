using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Wall_Detection : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D coll)
    {                                             //Wall                                      Ground
        if (coll.gameObject.layer.ToString() == "10" || coll.gameObject.layer.ToString() == "11")
        {
            if (gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX == true)
            {
                transform.parent.GetComponent<SpriteRenderer>().flipX = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                //Move the wall detector in front of the NPC
                
            }
            else if (gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX == false)
            {
                transform.parent.GetComponent<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
