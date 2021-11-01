using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour
{
    public static SmartPool instance;

    private List<GameObject> bullet_FX_Prefab = new List<GameObject>();
    private List<GameObject> bullet_Prefab = new List<GameObject>();
    private List<GameObject> Rocket_Prefab = new List<GameObject>();

    public GameObject[] zombies;
    private float y_Spawn_Pos_Min = -3.7f, y_Spawn_pos_max = -0.36f;

    private Camera mainCamera;
    void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        mainCamera = Camera.main;

        InvokeRepeating("StartSpwanZombies", 0, Random.Range(1f, 5f));
    }

    private void OnDisable()
    {
        instance = null;
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;

        }
    }

    public void CreateBulletAndBulletFall(GameObject bullet, GameObject bulletFall, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp_Bullet = Instantiate(bullet);
            GameObject temp_Bullet_Fall = Instantiate(bulletFall);

            bullet_FX_Prefab.Add(temp_Bullet_Fall);
            bullet_Prefab.Add(temp_Bullet);

            bullet_FX_Prefab[i].SetActive(false);
            bullet_Prefab[i].SetActive(false);
        }
    }

    public void CreateRocketBullet(GameObject rocket,  int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp_Rocket = Instantiate(rocket);
           
            Rocket_Prefab.Add(temp_Rocket);

            Rocket_Prefab[i].SetActive(false);
        }
    }

    public GameObject SpawnBulletFXPrefab(Vector3 position, Quaternion rotation)
    {
    
        for(int i = 0; i< bullet_FX_Prefab.Count; i++)
        {
            if (!bullet_FX_Prefab[i].activeInHierarchy)
            {
                bullet_FX_Prefab[i].SetActive(true);
                bullet_FX_Prefab[i].transform.position = position;
                bullet_FX_Prefab[i].transform.rotation = rotation;

                return bullet_FX_Prefab[i];
            }
        }

        return null;
    }

    public void SpawnBullet(Vector3 position,Vector3 direction, Quaternion rotation, NameWeapon weaponName)
    {

        if(weaponName != NameWeapon.ROCKET)
        {
            for (int i = 0; i < bullet_Prefab.Count; i++)
            {
                if (!bullet_Prefab[i].activeInHierarchy)
                {
                    bullet_Prefab[i].SetActive(true);
                    bullet_Prefab[i].transform.position = position;
                    bullet_Prefab[i].transform.rotation = rotation;

                    bullet_Prefab[i].GetComponent<BulletController>().SetDirection(direction);

                      
                    //SEt bullet damage
                    SetBulletDamage(weaponName, bullet_Prefab[i]);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Rocket_Prefab.Count; i++)
            {
                if (!Rocket_Prefab[i].activeInHierarchy)
                {
                    Rocket_Prefab[i].SetActive(true);
                    Rocket_Prefab[i].transform.position = position;
                    Rocket_Prefab[i].transform.rotation = rotation;

                    //get the bullet script
                    Rocket_Prefab[i].GetComponent<BulletController>().SetDirection(direction);

                    //SEt bullet damage
                    SetBulletDamage(weaponName, Rocket_Prefab[i]);

                    break;
                }
            }
        }     
    }

    void SetBulletDamage(NameWeapon weaponName, GameObject bullet)
    {
        switch (weaponName)
        {
            case NameWeapon.PISTOL:
                bullet.GetComponent<BulletController>().damage = 2;
                break;

            case NameWeapon.MP5:
                bullet.GetComponent<BulletController>().damage = 3;
                break;

            case NameWeapon.M3:
                bullet.GetComponent<BulletController>().damage = 4;
                break;

            case NameWeapon.AK:
                bullet.GetComponent<BulletController>().damage = 5;
                break;

            case NameWeapon.AWP:
                bullet.GetComponent<BulletController>().damage = 10;
                break;

            case NameWeapon.ROCKET:
                bullet.GetComponent<BulletController>().damage = 10;
                break;

        }
    }

    void StartSpwanZombies()
    {
        if(GamePlayController.instance.gameGoal == GameGoal.DEFEND_FENCE)
        {
            float xPos = mainCamera.transform.position.x;
            xPos += 15f;

            float ypos = Random.Range(y_Spawn_Pos_Min, y_Spawn_pos_max);

            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector2(xPos, ypos), Quaternion.identity);
        }

        if (GamePlayController.instance.gameGoal == GameGoal.KILL_ZOMBIES || GamePlayController.instance.gameGoal == GameGoal.TIMER_COUNTDOWN || GamePlayController.instance.gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            float xPos = mainCamera.transform.position.x;
           
            if(Random.Range(0,2) > 0)
            {
                xPos += Random.Range(10f, 15f);
            }
            else
            {
                xPos -= Random.Range(10f, 15f);
            }

            float ypos = Random.Range(y_Spawn_Pos_Min, y_Spawn_pos_max);

            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector2(xPos, ypos), Quaternion.identity);
        }

        if (GamePlayController.instance.gameGoal == GameGoal.GAME_OVER)
        {
            CancelInvoke("StartSpwanZombies");
        }
    }
}
