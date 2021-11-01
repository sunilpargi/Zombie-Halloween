using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG)
        {
            GamePlayController.instance.coinCount++;
            gameObject.SetActive(false);
        }
    }
}
