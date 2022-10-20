using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    float posX = -100.0f;

    void Update()
    {
        if (transform.position.x < posX)
        {
            Destroy(gameObject);
            posX = posX + 10.0f;
        }
    }
}
