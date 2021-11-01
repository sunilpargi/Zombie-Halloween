using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private ZombieAnimation zombie_Animation;
    private ZombieMovement zombie_Movement;

    private Transform targetTransform;
    private bool canAttack;
    public bool zombie_Alive;

    public GameObject damage_Collider;

    public int zombieHealth = 10;
    public GameObject[] fxDead;

    private float timerAttack;

    private int fireDamage = 10;

    public GameObject coinCollectable;
    public GameObject healthCollider;
    void Start()
    {
        zombie_Movement = GetComponent<ZombieMovement>();
        zombie_Animation = GetComponent<ZombieAnimation>();
     
        zombie_Alive = true;

        if(GamePlayController.instance.zombieGoal == ZombieGoal.PLAYER)
        {
            targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        }
        else if(GamePlayController.instance.zombieGoal == ZombieGoal.FENCE)
        {
            GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);

            targetTransform = fences[UnityEngine.Random.Range(0, fences.Length)].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (zombie_Alive)
        {
            CheckDistance();
        }
      
    }

    private void CheckDistance()
    {
        if (targetTransform)
        {
            if(Vector2.Distance(targetTransform.position, transform.position) > 1.5f)
            {
                zombie_Movement.Move(targetTransform);
            }

            else
            {
                if (canAttack)
                {
                    zombie_Animation.Attack();


                    timerAttack += Time.deltaTime;

                    if(timerAttack > 0.4f)
                    {
                        timerAttack = 0;
                        AudioManager.instance.ZombieAttackSound();
                    }
                   
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = true;
        }

        if(target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            if (!zombie_Alive) return;
            zombie_Animation.Hurt();

            zombieHealth -= target.gameObject.GetComponent<BulletController>().damage;

            if(target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                target.gameObject.GetComponent<BulletController>().ExplosionFX();
            }

            if(zombieHealth <= 0)
            {
                zombie_Alive = false;
                zombie_Animation.Dead();

                StartCoroutine(DeactivateZombie());
            }         

            target.gameObject.SetActive(false); 
        }

        if (target.tag == TagManager.FIRE_BULLET_TAG)
        {
            if (!zombie_Alive) return;
            zombie_Animation.Hurt();

            zombieHealth -= fireDamage;

            if (zombieHealth <= 0)
            {
                zombie_Alive = false;
                zombie_Animation.Dead();

                StartCoroutine(DeactivateZombie());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = false ;
        }
    }

    public void DealDamage(int damage)
    {
        zombie_Animation.Hurt();

        zombieHealth -= damage;

        if (zombieHealth <= 0)
        {
            //healthCollider.SetActive(false);
            //transform.GetComponentInChildren<BoxCollider2D>().enabled = false;
            zombie_Alive = false;
            zombie_Animation.Dead();

            StartCoroutine(DeactivateZombie());
        }
    }



    public void ActivateDeadFx(int index)
    {
        fxDead[index].SetActive(true);

        if (fxDead[index].GetComponent<ParticleSystem>())
        {
            fxDead[index].GetComponent<ParticleSystem>().Play();
        }
    }

    IEnumerator DeactivateZombie()
    {
        AudioManager.instance.ZombieDieSound();

        yield return new WaitForSeconds(2f);

        if(GamePlayController.instance.gameGoal == GameGoal.KILL_ZOMBIES)
        {
          GamePlayController.instance.ZombieDied();

        }


        if(UnityEngine.Random.Range(0,10) > 6)
        {
            Instantiate(coinCollectable, transform.position, Quaternion.identity);
        }
       

        gameObject.SetActive(false);
    }

    void ActivateDamagePoint()
    {
        damage_Collider.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damage_Collider.SetActive(false);
    }
}
