using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private WeaponManager weaponManager;

    [HideInInspector]
    public bool canShoot;

    public bool isHoldAttack;


    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponManager.SwitchWeapon();
        }

        if (Input.GetMouseButton(0))
        {

            isHoldAttack = true;
        }
        else
        {
            weaponManager.ResetAttack();
            isHoldAttack = false;
        }
       
        if(isHoldAttack && canShoot)
        {
            weaponManager.Attack();
        }

    }
}
