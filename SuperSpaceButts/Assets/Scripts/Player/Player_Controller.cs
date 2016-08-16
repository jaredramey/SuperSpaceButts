using UnityEngine;
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
    public float moveForce = 0.0f;
    [SerializeField]
    public float jumpForce = 0.0f;
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

        //If the player isn't moving up or down then the jump animation shouldn't be playing
        //This is kinda hacky for some use cases so i'll have to test this a bit.
        if (playerBody.velocity.y == 0)
        {
            playerAnimator.SetBool("Jump", false);
        }
        //Set animation back to jumping if player is falling
        //Should make a custom animation for that laters
        else if (playerBody.velocity.y < 0 && playerAnimator.GetBool("Jump") == false)
        {
            playerAnimator.SetBool("Jump", true);
        }
    }
    void FixedUpdate()
    {
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
            if (col.gameObject.transform.position.y <= gameObject.transform.position.y)
            {

                //Set jump state back to false2
                playerAnimator.SetBool("Jump", false);
                //if so change canJump back to true
                canJump = !canJump;
            }
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
