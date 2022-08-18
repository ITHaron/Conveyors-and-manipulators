using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class destroyer_controller : MonoBehaviour
{
    public UnityEvent<string> destroyItem;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            destroyItem.Invoke(other.gameObject.GetComponent<item>().get_tag());
            Destroy(other.gameObject);
        }
    }
}