using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyor_move : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    private Rigidbody rBody;
    
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        var distance = Vector3.back * speed * Time.fixedDeltaTime;

        rBody.position += distance;
        rBody.MovePosition(pos);
    }
}
