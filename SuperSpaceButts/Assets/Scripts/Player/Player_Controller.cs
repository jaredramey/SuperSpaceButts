using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    #region Variables_Private
    [SerializeField]
    public float moveForce = 0.0f;
    [SerializeField]
    public float jumpForce = 0.0f;
    [SerializeField]
    [HideInInspector]
    //May or may not end up using this as a means to move the player around.
    //Doesn't seem like I will be right now but just in case it's the best way.
    private Vector2 Movement = new Vector2(0.0f, 0.0f);
    private Rigidbody2D playerBody;
    [HideInInspector]
    private bool canJump = true;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();

        InputManager.Instance.OnMoveForward.AddListener(Handle_OnMoveForward);
        InputManager.Instance.OnMoveBackward.AddListener(Handle_OnMoveBackward);
        InputManager.Instance.OnJump.AddListener(Handle_OnJump);
        InputManager.Instance.OnUse.AddListener(Handle_OnUse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Better collision check with ground for now. If I think of something better
    //or someone lets me know of a better way I will change it.
    void OnCollisionEnter2D(Collision2D col)
    {
        //Check to see if the player has hit the ground
        if (!canJump && col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //if so change canJump back to true
            canJump = !canJump;
        }
    }

    #region Listener_Handles
    private void Handle_OnMoveForward()
    {
        playerBody.AddForce(-(Vector3.left) * moveForce);
    }
    private void Handle_OnMoveBackward()
    {
        playerBody.AddForce((Vector3.left) * moveForce);
    }
    private void Handle_OnJump()
    {
        if (canJump)
        {
            playerBody.AddForce((Vector3.up) * jumpForce);
            canJump = !canJump;
        }
    }
    private void Handle_OnUse()
    {

    }
    #endregion
}
