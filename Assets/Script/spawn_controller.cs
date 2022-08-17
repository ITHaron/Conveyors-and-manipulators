using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_controller : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int box_count = 10;
    [SerializeField] private float time = 5f;

    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner(){
        if(box_count > 0)
        {
             for(;;) {
                box_count--;
                Instantiate(prefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(time);
            }
        }
    }
}

