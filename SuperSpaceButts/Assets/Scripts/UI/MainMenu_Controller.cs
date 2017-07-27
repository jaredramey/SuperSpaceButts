using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Controller : MonoBehaviour
{
    private GameObject pointer;
    private GameObject[] Positions;
    private int currentPosition = 0;
    

    // Use this for initialization
    void Start()
    {
        pointer = GameObject.Find("Pointer");
        Positions = GameObject.FindGameObjectsWithTag("MenuPositions");

        Array.Sort(Positions, ComparePositions);

        pointer.transform.position = Positions[currentPosition].transform.position;

        InputHandler_MainMenu.Instance.OnMenuUp.AddListener(Handle_OnMenuUp);
        InputHandler_MainMenu.Instance.OnMenuDown.AddListener(Handle_OnMenuDown);
        InputHandler_MainMenu.Instance.OnMenuSelect.AddListener(Handle_OnMenuSelect);
        InputHandler_MainMenu.Instance.OnMenuBack.AddListener(Handle_OnMenuBack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int ComparePositions(GameObject left, GameObject right)
    {
        return left.name.CompareTo(right.name);
    }

    private void Handle_OnMenuUp()
    {
        Debug.Log("Menu up\n");

        if(currentPosition > 0)
        {
            currentPosition--;
            pointer.transform.position = Positions[currentPosition].transform.position;
        }
    }

    private void Handle_OnMenuDown()
    {
        Debug.Log("Menu Down\n");
        if (currentPosition < Positions.Length - 1)
        {
            currentPosition++;
            pointer.transform.position = Positions[currentPosition].transform.position;
        }
    }

    private void Handle_OnMenuSelect()
    {
        Debug.Log("Menu Select\n");
    }

    private void Handle_OnMenuBack()
    {
        Debug.Log("Menu Back\n");
    }
}
