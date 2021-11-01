using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_HEALTH_TAG).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GamePlayController.instance.gameGoal != GameGoal.DEFEND_FENCE && GamePlayController.instance.gameGoal != GameGoal.GAME_OVER)
        {
            if (player)
            {
                Vector3 temp = transform.position;
                temp.x = player.position.x;

                transform.position = temp;
            }
        }

       
    }
}
