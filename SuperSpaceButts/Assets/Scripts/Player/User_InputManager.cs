﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class User_InputManager : MonoBehaviour
{
    private static User_InputManager instance = null;

    [SerializeField]
    [HideInInspector]
    public float horizontal = 0.0f;

    #region EventInitArea
    [HideInInspector]
    public UnityEvent OnMoveForward = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMoveBackward = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnJump = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnUse = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnZoomIn = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnzoomOut = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAttack = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEscape = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAddHealth = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAddPoints = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnTakeScreenShot = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnGetWorldPos = new UnityEvent();
    #endregion

    #region VarInitArea
    [SerializeField]
    public float jumpCoolDownTime = 0.0f;
    #endregion

    // Use this for initialization
    void Start()
    {

    }

    public static User_InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (User_InputManager)FindObjectOfType(typeof(User_InputManager));
                if (instance == null)
                {
                    instance = (new GameObject("User_InputManager")).AddComponent<User_InputManager>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        #region Player-Movement
        //Jump Up
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump.Invoke();
        }
        //Move Forward
        //Using GetKey instead of GetKeyDown so the event will keep getting called
        if (Input.GetKey(KeyCode.D))
        {
            OnMoveForward.Invoke();
        }
        //Move Backward
        //Using GetKey instead of GetKeyDown so the event will keep getting called
        if (Input.GetKey(KeyCode.A))
        {
            OnMoveBackward.Invoke();
        }
        #endregion

        #region Player-Commands
        //For the eventual use item key-bind
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUse.Invoke();
        }
        //For the eventual use of power up attacks
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnAttack.Invoke();
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.I))
        {
            OnZoomIn.Invoke();
        }
        //Zoom Out
        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.O))
        {
            OnzoomOut.Invoke();
        }
        #endregion

        //TODO: Hook up debug commands
        #region Debug-Application-Keys
        //Escape will be later used for a pause menu but for now just needs to exit application
        //so I'm going to label it as a debug key strike for now
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape.Invoke();
        }
        //Add health
        //Gain(G) Health(H)
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.H))
        {
            Debug.Log("Activating cheat: Add Health +1");
            OnAddHealth.Invoke();
        }
        //Add Points
        //Gain(G) Points(P)
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.P))
        {
            Debug.Log("Activating cheat: Add Points +10");
            OnAddPoints.Invoke();
        }
        //Take a screen shot and get world pos for later debugging purposes
        //once a build has been released
        if (Input.GetKeyDown(KeyCode.F12))
        {
            OnTakeScreenShot.Invoke();
            OnGetWorldPos.Invoke();
        }
        #endregion
    }
}
