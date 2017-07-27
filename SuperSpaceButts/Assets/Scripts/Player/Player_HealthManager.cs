using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Player_HealthManager : MonoBehaviour
{
    private static Player_HealthManager instance = null;

    #region Variables_Private
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Animator playerAnimator;
    private SpriteRenderer player = null;
    [SerializeField]
    private int playerHealth = 0;
    [SerializeField]
    private float flashSpeed = 1.0f;
    [SerializeField]
    private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField]
    private Color flashColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    [HideInInspector]
    private bool damaged = false;
    [HideInInspector]
    private GameObject[] healthHearts = null; //This should end up as {heart1, heart2, heart3}
    #endregion

    #region Event_InitArea
    [HideInInspector]
    public UnityEvent OnPlayerTakeDamage = new UnityEvent();
    #endregion

    //When the level starts the player should have three health
    void Start()
    {
        #region Variable_Init
        healthHearts = GameObject.FindGameObjectsWithTag("HealthHeart");
        if(healthHearts != null)
        {
            for(int i = 0; i <= 2; i++)
            {
                //Debug.Log(healthHearts[i].name);
            }
        }
        else
        {
            Debug.Log("Unable to locate health heart UI elements.");
        }
        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        Array.Sort(healthHearts, CompareHeartNames);
        playerHealth = 3;
        playerAnimator = GetComponent<Animator>();
        #endregion

        #region Add_Listeners
        User_InputManager.Instance.OnAddHealth.AddListener(Handle_OnCheatAddHealth);
        Instance.OnPlayerTakeDamage.AddListener(Handle_PlayerTakeDamage);
        #endregion
    }

    public static Player_HealthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Player_HealthManager)FindObjectOfType(typeof(Player_HealthManager));
                if (instance == null)
                {
                    instance = (new GameObject("Player_HealthManager")).AddComponent<Player_HealthManager>();
                }
            }
            return instance;
        }
    }

    void Update()
    {
        for(int i = 0; i <= 2; i++)
        {
            if (playerHealth >= 0)
            {
                //If current heart is equal to or less than current health then activate the heart UI
                if(i + 1 <= playerHealth)
                {
                    if(healthHearts[i].GetComponent<Image>().sprite.ToString() != fullHeart.ToString())
                    {
                        healthHearts[i].GetComponent<Image>().sprite = fullHeart;
                    }
                }
                //If current heart is greater then current health then deactivate the heart
                else if(i + 1 > playerHealth)
                {
                    if (healthHearts[i].GetComponent<Image>().sprite != emptyHeart)
                    {
                        healthHearts[i].GetComponent<Image>().sprite = emptyHeart;
                    }
                }
            }
            else
            {
                Debug.Log("Player be dead");
            }
        }
    }

    void LateUpdate()
    {

        //TODO: Finish working this snippet of code
        #region Damage_Flash
        if(damaged == true)
        {
            player.color = flashColor;
        }
        else if(player.color != defaultColor)
        {
            player.color = Color.Lerp(player.color, defaultColor, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        #endregion

        #region Animation_Update
        //if (playerAnimator.GetBool("Damaged") == true)
        //{

        //playerAnimator.SetBool("Damaged", false);
        //}
        #endregion
    }

    #region Custom_Functions
    //For sorting the array of found UI elements since Unity can't be trusted.
    private int CompareHeartNames(GameObject left, GameObject right)
    {
        return left.name.CompareTo(right.name);
    }

    private void HurtPlayer(int damage)
    {
        //playerAnimator.SetBool("Damaged", true);
        playerHealth = playerHealth - damage;
        damaged = true;
    }

    public void HealPlayer(int health)
    {
        playerHealth = playerHealth + health;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    private void Handle_OnCheatAddHealth()
    {
        playerHealth = playerHealth + 1;
        Debug.Log("Added health! Player health is now: " + playerHealth);
    }

    private void Handle_PlayerTakeDamage()
    {
        HurtPlayer(1);
    }
    #endregion
}
