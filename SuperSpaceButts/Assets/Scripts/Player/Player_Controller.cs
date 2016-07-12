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
    private Vector2 Movement = new Vector2(0.0f, 0.0f);
    private Rigidbody2D playerBody;
    [HideInInspector]
    private GameObject GroundCheck;
    [HideInInspector]
    private bool canJump = true;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        GroundCheck = new GameObject();
        #region GroundCheck_Creation
        if (GroundCheck)
        {
            GroundCheck.name = "GroundCheck";
            GroundCheck.layer = LayerMask.NameToLayer("Player") ;
            GroundCheck.AddComponent<CircleCollider2D>();
            GroundCheck.GetComponent<CircleCollider2D>().isTrigger = true;
            GroundCheck.GetComponent<CircleCollider2D>().radius = 1.0f;
            GroundCheck.transform.parent = gameObject.transform;
            GroundCheck.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.283f);
        }
        #endregion

        InputManager.Instance.OnMoveForward.AddListener(Handle_OnMoveForward);
        InputManager.Instance.OnMoveBackward.AddListener(Handle_OnMoveBackward);
        InputManager.Instance.OnJump.AddListener(Handle_OnJump);
        InputManager.Instance.OnUse.AddListener(Handle_OnUse);
    }

    // Update is called once per frame
    void Update()
    {
        //There's gotta be a better way to check this...
        //TODO: Figure which is better for checking when the player has landed.
        if (!canJump && GroundCheck.GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.NameToLayer("Ground")))
        {
            canJump = !canJump;
        }
    }

    #region Listener-Handles
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
        }
        canJump = !canJump;
    }
    private void Handle_OnUse()
    {

    }
    #endregion
}
