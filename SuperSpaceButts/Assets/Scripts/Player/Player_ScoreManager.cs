using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_ScoreManager : MonoBehaviour
{
    private static Player_ScoreManager instance = null;

    #region Inspector_Variables
    //Store number art
    public Sprite[] numbers = new Sprite[9];
    //Store UI elements
    private GameObject[] numsUI = new GameObject[2];
    #endregion

    #region Variables_Private
    [SerializeField]
    private int playerScore = 0;
    #endregion

    void Start()
    {
        #region Add_Listeners
        User_InputManager.Instance.OnAddPoints.AddListener(Handle_OnCheatAddPoints);
        #endregion

        #region SortUIElems
        numsUI = GameObject.FindGameObjectsWithTag("ScoreNumber");
        //GameObject.FindObjectsWithTag() doesn't return in a reliable manner. So I have to sort myself.
        Array.Sort(numsUI, CompareNumNames);
        #endregion
    }

    public static Player_ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Player_ScoreManager)FindObjectOfType(typeof(Player_ScoreManager));
                if (instance == null)
                {
                    instance = (new GameObject("Player_ScoreManager")).AddComponent<Player_ScoreManager>();
                }
            }
            return instance;
        }
    }

    void Update()
    {
        //Take the score and turn it into a char array
        char[] currentScore = playerScore.ToString().ToCharArray();

        //Loop through each UI element
        for (int i = 0; i <= 2; i++)
        {
            //If there are 3 digits
            if (currentScore.Length == 3)
            {
                    int test = int.Parse(currentScore[i].ToString());
                    numsUI[i].GetComponent<Image>().sprite = numbers[test];
            }
            //If there are 2 digits
            else if(currentScore.Length == 2)
            {
                if (i >= 1)
                {
                    int test = int.Parse(currentScore[i - 1].ToString());
                    numsUI[i].GetComponent<Image>().sprite = numbers[test];
                }
            }
            //If there is only one digit
            else if (currentScore.Length == 1)
            {
                if (i >= 2)
                {
                    int test = int.Parse(currentScore[i - 2].ToString());
                    numsUI[i].GetComponent<Image>().sprite = numbers[test];
                }
            }
            //Otherwise, the digit should stay 0
            else
            {
                //If char array is smaller then i then it should be 0
                numsUI[i].GetComponent<Image>().sprite = numbers[0];
            }
         }

        //switch(currentScore[i])
        //{
        //    case '0':
        //        numsUI[i].GetComponent<Image>().sprite = numbers[currentScore[i]];
        //        break;
        //    case '1':
        //        numsUI[i].GetComponent<Image>().sprite = numbers[currentScore[i]];
        //        break;
        //    default:
        //        Debug.Log("No sprite available");
        //        break;

        //}
    }

    #region Custom_Functions
    //For sorting the array of found UI elements since Unity can't be trusted.
    private int CompareNumNames(GameObject left, GameObject right)
    {
        return left.name.CompareTo(right.name);
    }

    //Add points to player score
    public void AddPoints(int pointsToAdd)
    {
        //Make sure score doesn't go over 999 (Max possible on UI)
        if (playerScore + pointsToAdd <= 999)
        {
            playerScore = playerScore + pointsToAdd;
        }
        else
        {
            playerScore = 999;
        }
    }

    public void TakePoints(int pointsToTake)
    {
        //Make sure score doesn't go under 0
        if (playerScore - pointsToTake >= 0)
        {
            playerScore = playerScore - pointsToTake;
        }
        else
        {
            playerScore = 0;
        }
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    //What other classes can call to give player points
    private void Handle_OnCheatAddPoints()
    {
        AddPoints(10);
    }
    #endregion
}
