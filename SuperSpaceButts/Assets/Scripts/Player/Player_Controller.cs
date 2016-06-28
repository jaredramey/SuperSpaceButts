using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    public float moveForce = 0.0f;
    [SerializeField]
    public float jumpForce = 0.0f;
    [SerializeField]
    [HideInInspector]
    private Vector2 Movement = new Vector2(0.0f, 0.0f);
    [SerializeField]
    [HideInInspector]
    private float dT = 0.0f;
    private Rigidbody2D playerBody;

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
        //playerBody.AddForce(Movement);
        playerBody.velocity = Movement;
        Movement = new Vector2(0.0f, 0.0f);
        dT = Time.deltaTime;
    }

    #region Listener-Handles
    private void Handle_OnMoveForward()
    {
        //rigidbody2D.AddForce(Vector3.up * jumpSpeed * Time.deltaTime)
        Movement = new Vector2((InputManager.Instance.horizontal * moveForce), 0.0f);
    }
    private void Handle_OnMoveBackward()
    {
        Movement = new Vector2((InputManager.Instance.horizontal * moveForce), 0.0f);
    }
    private void Handle_OnJump()
    {
        Movement = new Vector2(0.0f, jumpForce);
        playerBody.AddForce(Movement);
        Movement = new Vector2(0.0f, 0.0f);
    }
    private void Handle_OnUse()
    {

    }
    #endregion
}
