using static System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulator_controller : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float angle_inp = 90f;
    [SerializeField] private float angle_out = 90f;

    [SerializeField] private float rotating_angle;
    [SerializeField] private bool rotating = false;
    
    [SerializeField] private bool item_grab = false;
    [SerializeField] private GameObject available_item;
    [SerializeField] private Transform base_manipulator;
    [SerializeField] private Transform bone_hand_bottom;
    [SerializeField] private Transform bone_hand_top;

    void Start() {
        base_manipulator = transform.Find("bone_base");
        bone_hand_bottom = base_manipulator.Find("bone_hand_bottom");
        bone_hand_top = bone_hand_bottom.Find("bone_hand_top");
        rotating_angle = angle_inp;
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
            if (item_grab == false)
            {
                if (base_manipulator.localEulerAngles.y == angle_inp)
                {
                    if (available_item != null)
                    {
                        rotating_angle = Abs(angle_inp - angle_out);
                        StartCoroutine(grab_manipulator());
                    }
                }
                else
                {
                    rotating_angle = angle_inp - angle_out;
                    //StartCoroutine(rotate_manipulator());
                }  
            }
            else
            {
                //item_grab = false;
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

    IEnumerator grab_manipulator()
    {
        item_grab = true;
        float _speed = speed*2;
        float timeElapsed = 0;
        //Quaternion startRotation_bottom = bone_hand_bottom.rotation;
        //Quaternion targetRotation_bottom = Quaternion.Euler(-70, -90, 0);

        Quaternion startRotation_top = Quaternion.Euler(-70, 0, 0);
        Quaternion targetRotation_top = Quaternion.Euler(-20, 0, 0);

        while (timeElapsed < speed)
        {
            //bone_hand_bottom.rotation = Quaternion.Slerp(startRotation_bottom, targetRotation_bottom, timeElapsed / _speed);
            bone_hand_top.rotation = Quaternion.Slerp(Quaternion.Euler(-70, 0, 0), Quaternion.Euler(-20, 0, 0), timeElapsed / speed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //bone_hand_bottom.rotation = targetRotation_bottom;
        //bone_hand_top.rotation = targetRotation_top;
        item_grab = false;
        //StartCoroutine(rotate_manipulator());
    }
}
