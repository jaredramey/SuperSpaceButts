using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InputHandler_MainMenu : MonoBehaviour
{
    private static InputHandler_MainMenu instance = null;

    #region EventInitArea
    [HideInInspector]
    public UnityEvent OnMenuUp = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMenuDown = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMenuSelect = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMenuBack = new UnityEvent();
    #endregion

    public static InputHandler_MainMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (InputHandler_MainMenu)FindObjectOfType(typeof(InputHandler_MainMenu));
                if (instance == null)
                {
                    instance = (new GameObject("InputHandler_MainMenu")).AddComponent<InputHandler_MainMenu>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnMenuUp.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnMenuDown.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            OnMenuSelect.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            OnMenuBack.Invoke();
        }
    }
}
