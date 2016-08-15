using UnityEngine;
using System.Collections;

public class Player_HealthManager : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField]
    private int playerHealth = 0;

    //When the level starts the player should have three health
    void Start()
    {
        playerHealth = 3;
        playerAnimator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if (playerAnimator.GetBool("Damaged") == true)
        {
            playerAnimator.SetBool("Damaged", false);
        }
    }
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
}
