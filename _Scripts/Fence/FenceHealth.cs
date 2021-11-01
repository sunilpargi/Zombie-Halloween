using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceHealth : MonoBehaviour
{
    public int health = 100;

    public ParticleSystem wood_Break_Fx;
    public ParticleSystem wood_Explode_Fx;

    

    public void DealDamage(int damage)
    {

        health -= damage;
        wood_Break_Fx.Play();

        AudioManager.instance.FenceExxplosioveSound();

        if (health <= 0)
        {
            wood_Explode_Fx.Play();

            GamePlayController.instance.fenceDestroyed = true; 

            StartCoroutine(DeactivategameObject());
        }
    }

    IEnumerator DeactivategameObject()
    {
        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }
}
