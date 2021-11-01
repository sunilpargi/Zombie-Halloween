using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [HideInInspector]
    public int damage;

    private float speed = 60f;

    private WaitForSeconds wait_For_Time_Alive = new WaitForSeconds(2f);

    private IEnumerator coroutineDeactivate;

    private Vector3 direction;

    public  GameObject rocket_Explosion;
    void Start()
    {
        if(this.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            speed = 8f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }


    private void OnEnable()
    {
        coroutineDeactivate = WaitForDeactiavate();
        StartCoroutine(coroutineDeactivate);
    }

    private void OnDisable()
    {
        if(coroutineDeactivate != null)
        {
            StopCoroutine(coroutineDeactivate);
        }
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    public void ExplosionFX()
    {
        AudioManager.instance.FenceExxplosioveSound();
        Instantiate(rocket_Explosion, transform.position, Quaternion.identity);
    }

    IEnumerator WaitForDeactiavate()
    {
        yield return wait_For_Time_Alive;
        gameObject.SetActive(false);
    }
}
