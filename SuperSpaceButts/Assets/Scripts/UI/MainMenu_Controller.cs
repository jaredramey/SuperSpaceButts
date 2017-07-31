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
}

public class MainMenu_Controller : MonoBehaviour
{
    private GameObject pointer;

    //TOOD: Work off of these variables
    private List<MenuSelections> menu = new List<MenuSelections>();
    private GameObject[] menuSections;
    private int currentMenuSection = 0;
    

    // Use this for initialization
    void Start()
    {
        pointer = GameObject.Find("Pointer");

        menuSections = GameObject.FindGameObjectsWithTag("MenuSection");

        Array.Sort(menuSections, ComparePositions);

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

        for(int i = 0; i < menu.Count; i++)
        {
            if(i != 0)
            {
                menu[i].menuSelection.SetActive(false);
            }
        }

        pointer.transform.position = menu[currentMenuSection].positions[menu[currentMenuSection].currentPosition].transform.position;

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

        if(menu[currentMenuSection].currentPosition > 0)
        {
            menu[currentMenuSection].currentPosition--;
            pointer.transform.position = menu[currentMenuSection].positions[menu[currentMenuSection].currentPosition].transform.position;
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

        if (menu[currentMenuSection].currentPosition < menu[currentMenuSection].positions.Count - 1)
        {
            menu[currentMenuSection].currentPosition++;
            pointer.transform.position = menu[currentMenuSection].positions[menu[currentMenuSection].currentPosition].transform.position;
        }
    }

    private void Handle_OnMenuSelect()
    {
        Debug.Log("Menu Select\n");
        if(currentMenuSection < menu.Count - 1)
        {
            currentMenuSection++;
            if(menu[currentMenuSection].menuSelection.activeInHierarchy == false)
            {
                menu[currentMenuSection].menuSelection.SetActive(true);
            }
            pointer.transform.position = menu[currentMenuSection].positions[menu[currentMenuSection].currentPosition].transform.position;
        }
    }

    private void Handle_OnMenuBack()
    {
        Debug.Log("Menu Back\n");
        if (currentMenuSection > 0)
        {
            if (menu[currentMenuSection].menuSelection.activeInHierarchy == true)
            {
                menu[currentMenuSection].menuSelection.SetActive(false);
            }
            currentMenuSection--;
            pointer.transform.position = menu[currentMenuSection].positions[menu[currentMenuSection].currentPosition].transform.position;
        }
    }
}
