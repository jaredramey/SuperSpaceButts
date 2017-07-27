using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraController : MonoBehaviour
{
    //Leaving this public for instances where I don't want the camera to move
    public bool isFollowing { get; set; }

    #region Variables_Private
    private Transform player;
    private BoxCollider2D cameraBounds;
    [SerializeField]
    private Vector2 margin;
    [SerializeField]
    private Vector2 smoothing;
    [SerializeField]
    [HideInInspector]
    private Vector3 min;
    [SerializeField]
    [HideInInspector]
    private Vector3 max;
    [SerializeField]
    [HideInInspector]
    private float dt;
    [SerializeField]
    private int maxZoom = 0;
    [SerializeField]
    private int minZoom = 0;
    [SerializeField]
    [HideInInspector]
    private int currentZoom;
    #endregion

    // Use this for initialization
    void Start()
    {
        #region Input_Listeners
        User_InputManager.Instance.OnZoomIn.AddListener(Handle_OnZoomIn);
        User_InputManager.Instance.OnzoomOut.AddListener(Handle_OnZoomOut);
        #endregion

        #region Variable_Init
        //Set currentZoom to midZoom to start
        currentZoom = maxZoom - minZoom;
        Camera.main.orthographicSize = currentZoom;

        player = GameObject.Find("Player").gameObject.transform;
        //Later will go back to main menu but for now just exit the game
        //TODO: Add a script deticated to debuging funtions
        if(!player)
        {
            Debug.Log("Player not found! Exiting application....");
            //TODO: Exit Application
        }

        cameraBounds = GetComponent<BoxCollider2D>();
        if(!cameraBounds)
        {
            Debug.Log("Camera Bounds not found! Exiting application....");
            //TODO: Exit Application
        }

        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
        isFollowing = true;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Variable_Updates
        float x = transform.position.x;
        float y = transform.position.y;
        dt = Time.deltaTime;
        #endregion

        #region Camera_Follow_Update
        if (isFollowing)
        {
            //If camera is outside the margine specified then smooth it so it doesn't go outside of it
            if(Mathf.Abs(x - player.position.x) > margin.x)
            {
                x = Mathf.Lerp(x, player.position.x, smoothing.x * dt);
            }
            if(Mathf.Abs(y - player.position.y) > margin.y)
            {
                y = Mathf.Lerp(y, player.position.y, smoothing.y * dt);
            }
        }
        #endregion

        #region Camera_Update
        //Get half the width of the camera
        float cameraHalfWidth = Camera.main.orthographicSize * ((float) Screen.width/ Screen.height);

        //Clamp the camera
        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + cameraHalfWidth, max.y - cameraHalfWidth);

        transform.position = new Vector3(x, y, transform.position.z);
        #endregion
    }

    #region Handle_Input
    //Allow the player to Zoom in and Out as wanted (To an extent)
    private void Handle_OnZoomOut()
    {
        //Debug.Log("Zooming in");
        if(currentZoom < maxZoom)
        {
            currentZoom += 1;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 1);
        }
    }
    private void Handle_OnZoomIn()
    {
        //Debug.Log("Zooming out");
        if (currentZoom > minZoom)
        {
            currentZoom -= 1;
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 1);
        }
    }
    #endregion
}
