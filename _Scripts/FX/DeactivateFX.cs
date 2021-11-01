using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFX : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("DeactivateGameObject", 2f);
    }

  void DeactivateGameObject()
    {

        gameObject.SetActive(false);
    }
}
