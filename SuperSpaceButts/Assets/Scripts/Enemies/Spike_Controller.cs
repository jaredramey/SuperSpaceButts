using UnityEngine;
using System.Collections;

public class Spike_Controller : MonoBehaviour
{
    #region Variables_Private
    private GameObject player;
    [SerializeField]
    private int damageToDeal = 0;
    #endregion

    // Use this for initialization
    void Start()
    {
        #region Variable_Init
        player = GameObject.Find("Player");
        #endregion
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        #region Hurt_Player
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.GetComponent<Player_HealthManager>().HurtPlayer(damageToDeal);
        }
        #endregion
    }

}
