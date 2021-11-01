using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponController> weapon_Unlocked;
    public WeaponController[] total_Weapons;

    [HideInInspector]
    public WeaponController current_Weapon;
    private int current_Weapon_index;

    private TypeControlAttack current_Type_Control;

    private PlayerArmControl[] armController;

    private PlayerAnimation playerAnim;

    private bool isShooting;

    public GameObject meleeDamagePoint;
    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnimation>();

        LoadActiveWeapon();

        current_Weapon_index = 1;
    }
    void Start()
    {
        armController = GetComponentsInChildren<PlayerArmControl>();

        ChangeWeapon(weapon_Unlocked[1]);

        playerAnim.SwitchWeaponAnimation((int)weapon_Unlocked[current_Weapon_index].defaultConfig.typeWeapon);
    }

    void LoadActiveWeapon()
    {
        weapon_Unlocked.Add(total_Weapons[0]);

        for (int i = 1; i < total_Weapons.Length; i++)
        {
            weapon_Unlocked.Add(total_Weapons[i]);
        }
    }

    public void SwitchWeapon()
    {
        current_Weapon_index++;
        current_Weapon_index = (current_Weapon_index == weapon_Unlocked.Count) ? 0 : current_Weapon_index;

        playerAnim.SwitchWeaponAnimation((int)weapon_Unlocked[current_Weapon_index].defaultConfig.typeWeapon);

        ChangeWeapon(weapon_Unlocked[current_Weapon_index]);
    }


    void ChangeWeapon(WeaponController newWeapon)
    {

        if (current_Weapon)
        {
            current_Weapon.gameObject.SetActive(false);
        }

        current_Weapon = newWeapon;
        current_Type_Control = newWeapon.defaultConfig.typeControlAttack;

        newWeapon.gameObject.SetActive(true);

        if(newWeapon.defaultConfig.typeWeapon == TypeWeapon.TwoHand)
        {
            for(int i = 0; i < armController.Length; i++)
            {
                armController[i].ChangeToTwoHand();
            }
        }
        else
        {
            for (int i = 0; i < armController.Length; i++)
            {
                armController[i].ChangeToOneHand();
            }
        }
    }


   public void Attack()
    {
        if(current_Type_Control == TypeControlAttack.Hold)
        {
          
            current_Weapon.CallAttack();
          
        }

        else if(current_Type_Control == TypeControlAttack.Click)
        {
            if (!isShooting)
            {
               
                current_Weapon.CallAttack();

                isShooting = true;
            }
        }
    }

   public void ResetAttack()
    {
        isShooting = false;
    }
 
    void AllowCollisionDetection()
    {
        meleeDamagePoint.SetActive(true);

    }

    void DenyColisionDetection()
    {
        meleeDamagePoint.SetActive(false);
    }

}
