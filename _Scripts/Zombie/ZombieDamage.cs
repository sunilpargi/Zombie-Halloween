using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    public LayerMask collisonMask;
    public float radius = 1f;

    public int damage = 3;

    private bool playerDead;


    void Update()
    {
        if(GamePlayController.instance.zombieGoal == ZombieGoal.PLAYER)
        {
            AttackPlayer();
        }

        if (GamePlayController.instance.zombieGoal == ZombieGoal.FENCE)
        {
            AttackFence();
        }


       
    }

    void AttackPlayer()
    {
        if(GamePlayController.instance.playerAlive)
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisonMask);

            if (target != null)
            {
                if (target.tag == TagManager.PLAYER_HEALTH_TAG)
                {
                    target.GetComponent<PlayerHealth>().DealDamage(damage);
                }
            }
        } 
    }

    void AttackFence()
    {
        if (!GamePlayController.instance.fenceDestroyed)
        {

            Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisonMask);

            if (target != null)
            {
                if (target.tag == TagManager.FENCE_TAG)
                {
                    target.GetComponent<FenceHealth>().DealDamage(damage);
                }
            }
        }
    }
    //private void OnEnable()
    //{
    //    PlayerHealth.playerDead += PlayerDeadListner;
    //}

    //private void OnDisable()
    //{
    //    PlayerHealth.playerDead -= PlayerDeadListner;
    //}
    //void PlayerDeadListner(bool dead)
    //{
    //    playerDead = dead;
    //}
}
