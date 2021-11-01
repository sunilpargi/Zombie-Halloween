using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponController : WeaponController
{
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public ParticleSystem fx_Shot;
    public GameObject fx_BulletFall;

    private Collider2D fireCollider;

    private WaitForSeconds wait_Time = new WaitForSeconds(0.02f);
    private WaitForSeconds fire_Collider_Wait = new WaitForSeconds(0.02f);
    void Start()
    {
        if (!GamePlayController.instance.bullet_And_Bullet_FX)
        {
            GamePlayController.instance.bullet_And_Bullet_FX = true;

            if (nameWeapon != NameWeapon.FIRE && nameWeapon != NameWeapon.ROCKET)
            {
                SmartPool.instance.CreateBulletAndBulletFall(bulletPrefab, fx_BulletFall, 100);
            }
        }


        if (!GamePlayController.instance.rocket_Bullet_Created)
        {
           

            if (nameWeapon == NameWeapon.ROCKET)
            {
                GamePlayController.instance.rocket_Bullet_Created = true;
                SmartPool.instance.CreateRocketBullet(bulletPrefab, 100);
            }
        }

        if(nameWeapon == NameWeapon.FIRE)
        {
            fireCollider = spawnPoint.gameObject.GetComponent<BoxCollider2D>();
        }
    }




    public override void ProcessAttack()
    {
        base.ProcessAttack();

        switch (nameWeapon)
        {
            case NameWeapon.PISTOL:
                AudioManager.instance.GunSound(0);
                break;

            case NameWeapon.MP5:
                AudioManager.instance.GunSound(1);
                break;

            case NameWeapon.M3:
                AudioManager.instance.GunSound(2);
                break;

            case NameWeapon.AK:
                AudioManager.instance.GunSound(3);
                break;

            case NameWeapon.AWP:
                AudioManager.instance.GunSound(4); 
                break;

            case NameWeapon.FIRE:
                AudioManager.instance.GunSound(5);
                break;

            case NameWeapon.ROCKET:
                AudioManager.instance.GunSound(6);
                break;
        }
    

        if(transform!= null && (nameWeapon != NameWeapon.FIRE))
        {
            if(nameWeapon != NameWeapon.ROCKET)
            {
                GameObject bullet_FallFX = SmartPool.instance.SpawnBulletFXPrefab(spawnPoint.transform.position, Quaternion.identity);

                bullet_FallFX.transform.localScale = (transform.rotation.eulerAngles.y > 1.0f) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

                StartCoroutine(WaitForShootEffect()); 
            }

            SmartPool.instance.SpawnBullet(spawnPoint.transform.position, new Vector3(-transform.root.localScale.x, 0, 0), spawnPoint.rotation, nameWeapon);
        } 
        else
        {
            StartCoroutine(ActivateFireCollider());
        }
    }

    IEnumerator WaitForShootEffect()
    {
        yield return wait_Time;
    } 

    IEnumerator ActivateFireCollider()
    {
        fireCollider.enabled = true;

        fx_Shot.Play();
        yield return fire_Collider_Wait;
        fireCollider.enabled = false;
    }
}
