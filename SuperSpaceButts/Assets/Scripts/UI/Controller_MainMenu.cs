using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MenuOption
{
    public GameObject currentMenuSelection = null;
    public List<MenuOption> childrenMenuOptions;
    public bool hasChildrenOptions = false, hasParentOption = false;
    public int currentChild = 0, locationInList = 0, parentLocationInList = 0;

    //Default constructor for clarity and definition
    public MenuOption()
    { }

    //Turn children of current menu option on or off
    public void ToggleChildrenOnOff()
    {
        //Loop through each child
        foreach(MenuOption child in childrenMenuOptions)
        {
            //If the object is active, turn it off
            if(child.currentMenuSelection.gameObject.activeInHierarchy == true)
            {
                child.currentMenuSelection.gameObject.SetActive(false);
            }
            //If object isn't active, turn it on
            else
            {
                child.currentMenuSelection.gameObject.SetActive(true);
            }
        }
    }
}

/*
 * This is attempt 2 at the UI system that I want to make for the main menu.
*/
public class Controller_MainMenu : MonoBehaviour
{
    private GameObject menuPointer;

    //List of menu options
    private List<MenuOption> menu;
    private GameObject[] foundMenuSections;
    private int currentMainOption = 0; //Shouldn't go past 2 (0 = Play, 1 = Options, 2 = Exit)

    // Use this for initialization
    void Start()
    {
        //Set handlers up for menu input
        InputHandler_MainMenu.Instance.OnMenuUp.AddListener(Handle_OnMenuUp);
        InputHandler_MainMenu.Instance.OnMenuDown.AddListener(Handle_OnMenuDown);
        InputHandler_MainMenu.Instance.OnMenuSelect.AddListener(Handle_OnMenuSelect);
        InputHandler_MainMenu.Instance.OnMenuBack.AddListener(Handle_OnMenuBack);

        
        //Get the main menu options
        foundMenuSections = GameObject.FindGameObjectsWithTag("MenuOption");
        Array.Sort(foundMenuSections, CompareNames);

        //Turn found objects into data for menu list
        for(int i = 0; i < foundMenuSections.Length; i++)
        {
            //Loop through and create menu options
            menu.Add(CreateMenuOption(foundMenuSections[i]));

            //Set location in list
            menu[i].locationInList = i;

            //Set the rest of the menu object variables

            //Check to see if menu object has a parent
            if(menu[i].currentMenuSelection.transform.parent != null)
            {
                menu[i].hasParentOption = true;

                //Loop through menu options to find parent in list and add it.
                for(int j = 0; j < menu.Count; j++)
                {
                    if(menu[j].currentMenuSelection.name == menu[i].currentMenuSelection.transform.parent.name)
                    {
                        Debug.Log("Parent found for " + menu[i].currentMenuSelection.name + "!");
                        Debug.Log("Parent = " + menu[j].currentMenuSelection.name);
                        menu[i].parentLocationInList = menu[j].locationInList;
                    }
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private MenuOption CreateMenuOption(GameObject MenuOption)
    {
        //Create new blank MenuOption object
        MenuOption temp = new MenuOption();

        //Set what object this is referencing
        temp.currentMenuSelection = MenuOption;

        //Loop through each child object
        for(int i = 0; i < MenuOption.transform.childCount; i++)
        {
            //Add each child to the list of children options after checking to make sure it's not a position object.
            if (temp.currentMenuSelection.transform.GetChild(i).tag == "MenuSection")
            {
                Debug.Log("Current menu object: " + temp.currentMenuSelection.name + " -> " + temp.currentMenuSelection.transform.GetChild(i).name);
                MenuOption tempChild = CreateMenuOption(MenuOption.transform.GetChild(i).gameObject);

                if (tempChild != null)
                {
                    temp.childrenMenuOptions.Add(tempChild);

                    if (temp.hasChildrenOptions == false)
                    {
                        temp.hasChildrenOptions = true;
                    }
                }
            }
        }


        if (temp.currentMenuSelection != null)
        {
            return temp;
        }
        else
        {
            return null;
        }
    }

    private int CompareNames(GameObject left, GameObject right)
    {


        return left.name.CompareTo(right.name);
    }

    private void Handle_OnMenuUp()
    {
        Debug.Log("Menu up\n");
    }

    private void Handle_OnMenuDown()
    {
        Debug.Log("Menu Down\n");
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
