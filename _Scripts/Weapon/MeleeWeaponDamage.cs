using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 3f;

    public int damage = 3;


    // Update is called once per frame
    void Update()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

        if(target != null)
        {
            if (target.tag == TagManager.ZOMBIE_HEALTH_TAG && target.GetComponent<ZombieController>().zombie_Alive == true)
            {
                target.transform.root.GetComponent<ZombieController>().DealDamage(damage);
            }
        }
       
       
    }
}
