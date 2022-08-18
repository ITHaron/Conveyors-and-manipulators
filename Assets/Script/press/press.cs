using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class press : MonoBehaviour
{
    public UnityEvent<GameObject> CheckItem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            CheckItem.Invoke(other.gameObject);
        }
    }
}
