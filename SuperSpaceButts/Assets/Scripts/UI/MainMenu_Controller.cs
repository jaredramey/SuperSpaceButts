using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MenuSelections
{
    public GameObject menuSelection;
    public List<GameObject> menuOptions = new List<GameObject>();
    public List<GameObject> positions = new List<GameObject>();
    public int currentPosition = 0;
    public bool isActive = true;
}

public class MainMenu_Controller : MonoBehaviour
{
    private GameObject pointer;

    //TOOD: Work off of these variables
    private List<MenuSelections> menu = new List<MenuSelections>();
    private GameObject[] menuSections;
    

    // Use this for initialization
    void Start()
    {
        pointer = GameObject.Find("Pointer");

        menuSections = GameObject.FindGameObjectsWithTag("MenuSection");

        foreach(GameObject menuSection in menuSections)
        {
            MenuSelections temp = new MenuSelections();
            temp.menuSelection = menuSection;
            Debug.Log(temp.menuSelection.transform.childCount);
            foreach(Transform child in temp.menuSelection.transform)
            {
                temp.menuOptions.Add(child.gameObject);
                temp.positions.Add(child.transform.GetChild(0).gameObject);
            }
            Debug.Log("menuOptions size = " + temp.menuOptions.Count + "\n" +
                      "positions size = " + temp.positions.Count + "\n");
            menu.Add(temp);
        }

        pointer.transform.position = menu[0].positions[0].transform.position;

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
        //if(currentPosition > 0)
        //{
        //    currentPosition--;
        //    pointer.transform.position = Positions[currentPosition].transform.position;
        //}

        if(menu[0].currentPosition > 0)
        {
            menu[0].currentPosition--;
            pointer.transform.position = menu[0].positions[menu[0].currentPosition].transform.position;
        }
    }

    private void Handle_OnMenuDown()
    {
        Debug.Log("Menu Down\n");
        //if (currentPosition < Positions.Length - 1)
        //{
        //    currentPosition++;
        //    pointer.transform.position = Positions[currentPosition].transform.position;
        //}

        if (menu[0].currentPosition < menu[0].positions.Count)
        {
            menu[0].currentPosition++;
            pointer.transform.position = menu[0].positions[menu[0].currentPosition].transform.position;
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
