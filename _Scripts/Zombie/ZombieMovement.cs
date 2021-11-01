using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private float move_Speed = 1f;
     public void Move(Transform targetTransform)
    {
        Flip(targetTransform);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetTransform.position.x, targetTransform.position.y - 0.9f, targetTransform.position.z), move_Speed * Time.deltaTime);
    }

    void Flip(Transform targetTransform)
    {

        Vector3 tempPosition = transform.position;
        Vector3 tempScale = transform.localScale;

        if(targetTransform.position.x > (tempPosition.x + 0.08f))
        {
            tempScale.x = -1f;
        }
        else if(targetTransform.position.x < (tempPosition.x - 0.08f))
        {
            tempScale.x = 1f;
        }

        transform.localScale = tempScale;
    }

}
