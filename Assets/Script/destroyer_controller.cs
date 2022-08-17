using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer_controller : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "box")
        {
            print(collider.gameObject);
            Destroy(collider.gameObject);
        }
    }
}