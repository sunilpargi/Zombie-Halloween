using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkBound : MonoBehaviour
{
    public float min_X, max_x;
    void Update()
    {
        MoveMentBounds();
    }

    private void MoveMentBounds()
    {
        Vector3 temp = transform.position;

        if(temp.x < min_X)
        {
            temp.x = min_X;
        }

        if(temp.x > max_x)
        {
            temp.x = max_x;
        }

        transform.position = temp;
    }

 
}
