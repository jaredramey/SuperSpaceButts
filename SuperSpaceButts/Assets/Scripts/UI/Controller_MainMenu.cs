using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MenuOption
{
    public GameObject currentMenuSelection;
    public List<GameObject> childrenMenuOptions;
    public bool hasChildrenOptions = false;
    public int currentChild = 0;
}

public class Controller_MainMenu : MonoBehaviour
{
    private GameObject menuPointer;

    private List<MenuOption> menu;
    private MenuOption[] options = new MenuOption[3];
    private LinkedList<MenuOption> MainSections = new LinkedList<MenuOption>();
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
            //Add each child to the list of children objects
            temp.childrenMenuOptions.Add(MenuOption.transform.GetChild(i).gameObject);
        }
        
        if(temp.childrenMenuOptions.Count > 0)
        {
            temp.hasChildrenOptions = true;
        }

        return temp;
    }

    private int ComparePositions(GameObject left, GameObject right)
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
