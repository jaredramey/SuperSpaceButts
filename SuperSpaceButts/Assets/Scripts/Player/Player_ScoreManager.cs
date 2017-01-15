using UnityEngine;
using System.Collections;

public class Player_ScoreManager : MonoBehaviour
{
    #region Variables_Private
    [SerializeField]
    private int playerScore = 0;
    #endregion

    void Start()
    {
        #region Add_Listeners
        User_InputManager.Instance.OnAddPoints.AddListener(Handle_OnCheatAddPoints);
        #endregion
    }

    #region Custom_Functions
    public void AddPoints(int pointsToAdd)
    {
        playerScore = playerScore + pointsToAdd;
    }

    public void TakePoints(int pointsToTake)
    {
        playerScore = playerScore - pointsToTake;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    private void Handle_OnCheatAddPoints()
    {
        playerScore = playerScore + 10;
        Debug.Log("Added points! Player score is now: " + playerScore);
    }
    #endregion
}
