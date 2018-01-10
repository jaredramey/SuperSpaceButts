using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MenuOption
{
    public GameObject currentMenuSelection = null;
    public List<MenuOption> childrenMenuOptions = new List<MenuOption>();
    public bool hasChildrenOptions = false, hasParentOption = false;
    public int locationInList = 0, parentLocationInList = 0;

    //Default constructor for clarity and definition
    public MenuOption()
    { }

    //Get the current menu selection's pointer position
    public Vector3 GetMenuPointerPosition()
    {
        return currentMenuSelection.transform.GetChild(0).transform.position;
    }

    //Turn children of current menu option on or off
    public void ToggleChildrenOn()
    {
        //Loop through each child
        foreach(MenuOption child in childrenMenuOptions)
        {
            child.currentMenuSelection.gameObject.SetActive(true);
        }
    }

    public void ToggleChildrenOff()
    {
        foreach (MenuOption child in childrenMenuOptions)
        {
            child.currentMenuSelection.gameObject.SetActive(false);
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
    private List<MenuOption> menu = new List<MenuOption>();
    private GameObject[] foundMenuSections;
    private int currentMenuOption = 0, parentChildrenCount = 0, currentSectionCount = 0;
    

    // Use this for initialization
    void Start()
    {
        //Set handlers up for menu input
        InputHandler_MainMenu.Instance.OnMenuUp.AddListener(Handle_OnMenuUp);
        InputHandler_MainMenu.Instance.OnMenuDown.AddListener(Handle_OnMenuDown);
        InputHandler_MainMenu.Instance.OnMenuSelect.AddListener(Handle_OnMenuSelect);
        InputHandler_MainMenu.Instance.OnMenuBack.AddListener(Handle_OnMenuBack);

        //Get the pointer game object
        menuPointer = GameObject.Find("Pointer");
        
        //Get the main menu options
        foundMenuSections = GameObject.FindGameObjectsWithTag("MenuOption");
        Array.Sort(foundMenuSections, CompareNames);

        parentChildrenCount = foundMenuSections.Length - 1;

        //Turn found objects into data for menu list
        for(int i = 0; i < foundMenuSections.Length; i++)
        {
            MenuOption temp = new MenuOption();
                
            temp = CreateMenuOption(foundMenuSections[i]);

            //Loop through and create menu options
            menu.Add(temp);            
        }

        for (int i = 0; i < menu.Count; i++)
        {
            foreach(MenuOption child in menu[i].childrenMenuOptions)
            {
                child.parentLocationInList = i;
                menu.Add(child);
            }

            menu[i].locationInList = i;
            
            if(menu[i].hasChildrenOptions)
            {
                menu[i].ToggleChildrenOff();
            }
        }

        //Set pointer location to first menu option (play)
        menuPointer.transform.position = menu[0].GetMenuPointerPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetPositionsInList()
    {

    }

    private void UpdatePointerPos()
    {
        menuPointer.transform.position = menu[currentMenuOption].currentMenuSelection.transform.GetChild(0).position;
    }

    private MenuOption CreateMenuOption(GameObject MenuOption)
    {
        //Create new blank MenuOption object
        MenuOption temp = new MenuOption();

        //Set what object this is referencing
        temp.currentMenuSelection = MenuOption;

        if (temp.currentMenuSelection.transform.childCount > 1)
        {
            //Loop through each child object
            for (int i = 0; i < MenuOption.transform.childCount; i++)
            {
                //Add each child to the list of children options after checking to make sure it's not a position object.
                if (temp.currentMenuSelection.transform.GetChild(i).tag == "MenuSection")
                {
                    MenuOption tempChild = CreateMenuOption(MenuOption.transform.GetChild(i).gameObject);

                    if (tempChild != null)
                    {
                        tempChild.hasParentOption = true;

                        if (temp.hasChildrenOptions == false)
                        {
                            temp.hasChildrenOptions = true;
                        }
                        temp.childrenMenuOptions.Add(tempChild);
                    }  
                }

                //Check to see if there is a parent

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
        if(currentSectionCount > 0)
        {
            currentMenuOption--;
            currentSectionCount--;
            UpdatePointerPos();
        }
    }

    private void Handle_OnMenuDown()
    {
        if(currentSectionCount < parentChildrenCount)
        {
            currentSectionCount++;
            currentMenuOption++;
            UpdatePointerPos();
        }
    }

    private void Handle_OnMenuSelect()
    {
        if(menu[currentMenuOption].currentMenuSelection.name == "2_Options")
        {
            menuPointer.SetActive(false);
        }

        if(menu[currentMenuOption].hasChildrenOptions)
        {
            menu[currentMenuOption].ToggleChildrenOn();
            parentChildrenCount = menu[currentMenuOption].childrenMenuOptions.Count - 1;
            currentMenuOption = menu[currentMenuOption].childrenMenuOptions[0].locationInList;
            UpdatePointerPos();
            currentSectionCount = 0;
        }
        
        else if(menu[currentMenuOption].currentMenuSelection.name == "3_Exit")
        {
            //TODO: Add exit option
            Debug.Log("Exit game");
        }
        else if (menu[currentMenuOption].currentMenuSelection.name == "World_1-1")
        {
            //TODO: Add level option
            LevelManager.Instance.LoadNextLevel("Playground");
        }
        else if (menu[currentMenuOption].currentMenuSelection.name == "World_1-2")
        {
            //TODO: Add level option 2
        }
    }

    private void Handle_OnMenuBack()
    {
        if(menu[currentMenuOption].hasParentOption)
        {
            if(menuPointer.activeInHierarchy == false)
            {
                menuPointer.SetActive(true);
            }

            currentMenuOption = menu[currentMenuOption].parentLocationInList;
            menu[currentMenuOption].ToggleChildrenOff();
            if (menu[currentMenuOption].hasParentOption)
            {
                parentChildrenCount = menu[menu[currentMenuOption].parentLocationInList].childrenMenuOptions.Count - 1;
                currentSectionCount = 0;
            }
            else
            {
                parentChildrenCount = foundMenuSections.Length - 1;
                currentSectionCount = menu[currentMenuOption].locationInList;
            }
            UpdatePointerPos();
        }
    }
}
