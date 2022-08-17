using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyor_move : MonoBehaviour
{
    [SerializeField] private int speed = 1;
    [SerializeField] private bool upend = false;
    private Rigidbody rBody;
    
    
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        Vector3 distance;
        if(upend)
            distance = Vector3.right * speed * Time.fixedDeltaTime;
        else
            distance = Vector3.back * speed * Time.fixedDeltaTime;

        rBody.position += distance;
        rBody.MovePosition(pos);
    }
}
