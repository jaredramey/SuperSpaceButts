﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    #region Variables_Private
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    [SerializeField]
    public float downwardForce = 0.0f;
    [SerializeField]
    [HideInInspector]
    private float dt = 0.0f;
    [SerializeField]
    public float moveForce = 0.0f;
    [SerializeField]
    public float jumpForce = 0.0f;
    [SerializeField]
    [HideInInspector]
    //May or may not end up using this as a means to move the player around.
    //Doesn't seem like I will be right now but just in case it's the best way.
    private Vector2 Movement = new Vector2(0.0f, 0.0f);
    [HideInInspector]
    private bool canJump = true;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();

        InputManager.Instance.OnMoveForward.AddListener(Handle_OnMoveForward);
        InputManager.Instance.OnMoveBackward.AddListener(Handle_OnMoveBackward);
        InputManager.Instance.OnJump.AddListener(Handle_OnJump);
        InputManager.Instance.OnUse.AddListener(Handle_OnUse);
    }

    // Update is called once per frame
    void Update()
    {
        //Update the float so the animation plays
        //using abs to make it so the animation will go no matter which way the player runs
        playerAnimator.SetFloat("xSpeed", Mathf.Abs(playerBody.velocity.x));
    }
    void FixedUpdate()
    {
        dt = Time.deltaTime;

        //push the player down faster on decent
        if (playerBody.velocity.y < 0)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y * downwardForce);
        }
    }

    //Better collision check with ground for now. If I think of something better
    //or someone lets me know of a better way I will change it.
    void OnCollisionEnter2D(Collision2D col)
    {
        //Check to see if the player has hit the ground
        if (!canJump && col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //Set jump state back to false
            playerAnimator.SetBool("Jump", false);
            //if so change canJump back to true
            canJump = !canJump;
        }
    }

    #region Listener_Handles
    private void Handle_OnMoveForward()
    {
        //Add force to push the player right
        playerBody.AddForce(((Vector2.left) * moveForce) * -InputManager.Instance.horizontal);
        //Flip the player back to facing the right
        GetComponent<SpriteRenderer>().flipX = false;
    }
    private void Handle_OnMoveBackward()
    {
        //Add force to push the player left
        playerBody.AddForce(((Vector2.left) * moveForce) * -InputManager.Instance.horizontal);
        //Flip the player to face the left
        GetComponent<SpriteRenderer>().flipX = true;
    }
    private void Handle_OnJump()
    {
        if (canJump)
        {
            //Set jump state to true
            playerAnimator.SetBool("Jump", true);
            //Apply force to player
            playerBody.AddForce((Vector2.up) * jumpForce);
            //Make it so the player can't jump till he hit's the ground
            canJump = !canJump;
        }
    }
    private void Handle_OnUse()
    {

    }
    #endregion
}
