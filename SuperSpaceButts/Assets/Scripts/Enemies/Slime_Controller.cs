using UnityEngine;
using System.Collections;

public class Slime_Controller : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 0;
    [HideInInspector]
    private bool flipped = false;

    // Update is called once per frame
    void Update()
    {
        float speedToAdd = (Time.deltaTime * walkSpeed);

        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.x);

        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.left) * walkSpeed);
        }
        else if(gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-(Vector2.left) * walkSpeed);
        }

    }

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    Debug.Log("Collision");
    //    if (coll.gameObject.layer.ToString() == "Wall")
    //    {
    //        Debug.Log("Flipping X");
    //        if (flipped)
    //        {
    //            flipped = false;
    //            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    //        }
    //        else
    //        {
    //            flipped = true;
    //            gameObject.GetComponent<SpriteRenderer>().flipX = true;
    //        }
    //    }
    //}
}