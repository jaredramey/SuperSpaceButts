using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [HideInInspector]
    private int screenResolutionX = 400, screenResolutionY = 300;
    public int defaultScreenResolution = 1;

    void Start()
    {
        Debug.Log(Screen.currentResolution);
        //if(Screen.currentResolution)
    }

    public void UpdateScreenResolution(int currentSelection)
    {
        //5:4
        if(currentSelection == 0)
        {
            screenResolutionX = 500;
            screenResolutionY = 400;
        }
        //4:3
        else if (currentSelection == 1)
        {
            screenResolutionX = 400;
            screenResolutionY = 300;
        }
        //3:2
        else if (currentSelection == 2)
        {
            screenResolutionX = 300;
            screenResolutionY = 200;
        }
        //16:10
        else if (currentSelection == 3)
        {
            screenResolutionX = 1600;
            screenResolutionY = 1000;
        }
        //16:9
        else if (currentSelection == 4)
        {
            screenResolutionX = 1600;
            screenResolutionY = 900;
        }

        //Update Resolution
        Screen.SetResolution(screenResolutionX, screenResolutionY, Screen.fullScreen);
    }
}
