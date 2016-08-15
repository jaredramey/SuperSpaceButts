using UnityEngine;
using System.Collections;

public class Player_Score : MonoBehaviour
{
    [SerializeField]
    private int playerScore = 0;

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
}
