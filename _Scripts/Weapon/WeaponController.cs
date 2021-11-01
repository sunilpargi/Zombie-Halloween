using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum NameWeapon
{
    MELEE,
    PISTOL,
    MP5,
    M3,
    AK,
    AWP,
    FIRE,
    ROCKET

}
public class WeaponController : MonoBehaviour
{
    public DefaultConfig defaultConfig;
    public NameWeapon nameWeapon;

    public PlayerAnimation playerAnim;
    protected float lastShot;

    public int gunIndex;
    public int currentBullet;
    public int bulletMax;
    void Start()
    {
      //  playerAnim = GetComponentInParent<PlayerAnimation>();
        currentBullet = bulletMax;

    }

 
    public void CallAttack()
    {
        if(Time.time > lastShot + defaultConfig.fireRate)
        {
            if(currentBullet > 0)
            {
                ProcessAttack();

                playerAnim.AttackAnimation();


                lastShot = Time.time;
                currentBullet--;
            }
            else
            {

            }
        }
    }//callAttack

    public virtual void ProcessAttack()
    {

    }
} 
