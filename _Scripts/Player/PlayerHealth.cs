using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public GameObject[] bloodFX;

    private PlayerAnimation playerAnimation;

    //public delegate void PlayerDeadEvent(bool dead);
    //public static event PlayerDeadEvent playerDead;
    void Awake()
    {
        playerAnimation = GetComponentInParent<PlayerAnimation>();
    }


    public void DealDamage(int damage)
    {
        health -= damage;

        playerAnimation.Hurt();

        GamePlayController.instance.PlayerLifeCounter(health);

        if(health <= 0)
        {
            //if(playerDead != null)
            //{
            //    playerDead(true);
            //}

            GamePlayController.instance.playerAlive = false;

            GetComponent<BoxCollider2D>().enabled = false;
            playerAnimation.Dead();

            int index = Random.Range(0, bloodFX.Length);
            bloodFX[index].SetActive(true);

            GamePlayController.instance.GameOver();
            
        }
    }
 
}
