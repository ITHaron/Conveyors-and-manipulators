using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_controller : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float time = 5f;

    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        for(;;) {
                Instantiate(prefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(time);
        }
    }
}

