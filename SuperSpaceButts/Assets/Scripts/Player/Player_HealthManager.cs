using UnityEngine;
using System.Collections;

public class Player_HealthManager : MonoBehaviour
{
    #region Variables_Private
    private Animator playerAnimator;
    [SerializeField]
    private int playerHealth = 0;
    #endregion

    //When the level starts the player should have three health
    void Start()
    {
        #region Variable_Init
        playerHealth = 3;
        playerAnimator = GetComponent<Animator>();
        #endregion
    }

    void LateUpdate()
    {
        #region Animation_Update
        if (playerAnimator.GetBool("Damaged") == true)
        {
            playerAnimator.SetBool("Damaged", false);
        }
        #endregion
    }

    #region Custom_Functions
    public void HurtPlayer(int damage)
    {
        playerAnimator.SetBool("Damaged", true);
        playerHealth = playerHealth - damage;
    }

    public void HealPlayer(int health)
    {
        playerHealth = playerHealth + health;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
    #endregion
}
