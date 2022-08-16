using System.Collections;
using System.Collections.Generic;
using static System.Math;
using UnityEngine;

public class manipulator_controller : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float angle_inp = 90f;
    [SerializeField] private float angle_out = 180f;

    [SerializeField] private float rotating_angle;
    [SerializeField] private bool rotating = false;
    
    [SerializeField] private bool item_grab;

    [SerializeField] private GameObject available_item;
    [SerializeField] private Transform base_manipulator;

    void Start() {
        base_manipulator = transform.Find("bone_base");
        rotating_angle = angle_inp;
        base_manipulator.rotation = Quaternion.Euler(new Vector3(0, angle_inp, 0));
        print(base_manipulator.localEulerAngles.y);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "box")
        {
            available_item = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "box")
        {
            available_item = null;
            // if (base_manipulator.localEulerAngles.y == angle_inp)
            // {
            //     // StartCoroutine(item_grabing());
            //     rotating_angle = angle_out;
            //     StartCoroutine(rotate_manipulator());
            //     print(collider.gameObject.name);
            // }
        }
    }

    void FixedUpdate()
    {
        if(rotating == false)
        {
            print(base_manipulator.localEulerAngles.y);
            if (item_grab == false)
            {
                if (base_manipulator.localEulerAngles.y == angle_inp)
                {
                    if (available_item != null)
                    {
                        // StartCoroutine(item_grabing());
                        item_grab = true;
                        rotating_angle = Abs(angle_inp - angle_out);
                        print("inp");
                        StartCoroutine(rotate_manipulator());
                    }
                }
                else
                {
                    print("out");
                    rotating_angle = angle_inp - angle_out;
                    StartCoroutine(rotate_manipulator());
                }  
            }
            else
            {
                item_grab = false;
                // StartCoroutine(item_ungrabing());
            }
        }
    }

    IEnumerator rotate_manipulator()
    {
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = base_manipulator.rotation;
        Quaternion targetRotation = base_manipulator.rotation * Quaternion.Euler(0, rotating_angle, 0);
        while (timeElapsed < speed)
        {
            base_manipulator.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / speed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        base_manipulator.rotation = targetRotation;
        rotating = false;
    }
}
