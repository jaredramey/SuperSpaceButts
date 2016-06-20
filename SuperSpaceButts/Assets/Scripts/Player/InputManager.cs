using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;

    #region Event-Init-Area
    [HideInInspector]
    public UnityEvent OnMoveForward = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMoveBackward = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnJump = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnUse = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAttack = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEscape = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAddHealth = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAddPoints = new UnityEvent();
    #endregion

    #region Var-Init-Area
    [SerializeField]
    public float jumpCoolDownTime = 0.0f;
    [SerializeField]
    private float jumpTimer = 0.0f;
    private bool canJump = true;
    #endregion

    // Use this for initialization
    void Start()
    {

    }

    public static InputManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (InputManager)FindObjectOfType(typeof(InputManager));
                if(instance == null)
                {
                    instance = (new GameObject("InputManager")).AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Player-Movement
        //Jump Up
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJump.Invoke();
            canJump = !canJump;
        }
        //Move Forward
        if(Input.GetKeyDown(KeyCode.D))
        {
            OnMoveForward.Invoke();
        }
        //Move Backward
        if(Input.GetKeyDown(KeyCode.A))
        {
            OnMoveBackward.Invoke();
        }
        #endregion

        #region Player-Commands
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnUse.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnAttack.Invoke();
        }
        #endregion

        #region Debug-Application-Keys
        //Escape will be later used for a pause menu but for now just needs to exit application
        //so i'm going to label it as a debug key strike for now
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape.Invoke();
        }
        //Add health
        //Gain(G) Health(H)
        if(Input.GetKeyDown(KeyCode.G) && Input.GetKeyDown(KeyCode.H))
        {
            OnAddHealth.Invoke();
        }
        //Add Points
        //Gain(G) Points(P)
        if(Input.GetKeyDown(KeyCode.G) && Input.GetKeyDown(KeyCode.P))
        {
            OnAddPoints.Invoke();
        }
        #endregion
    }

    void FixedUpdate()
    {
        if(jumpTimer > 0 && !canJump)
        {
            jumpTimer--;
        }
        else if(jumpTimer <= 0)
        {
            jumpTimer = jumpCoolDownTime;
            canJump = !canJump;
        }
    }
}
