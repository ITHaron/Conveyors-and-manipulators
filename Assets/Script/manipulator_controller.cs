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
    
    [SerializeField] private bool is_grab = false;
    [SerializeField] private GameObject item_grab = null;
    [SerializeField] private GameObject available_item = null;
    [SerializeField] private Transform base_manipulator;
    // [SerializeField] private Transform bone_hand_bottom;
    // [SerializeField] private Transform bone_hand_top;

    void Start() {
        base_manipulator = transform.Find("bone_base");
        //bone_hand_top.rotation = Quaternion.Euler(-70, 0, 0);
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
            if (is_grab == false)
            {
                if (base_manipulator.localEulerAngles.y == angle_inp)
                {
                    if (available_item != null)
                    {
                        rotating_angle = Abs(angle_inp - angle_out);
                        StartCoroutine(down_manipulator());
                    }
                }
                else if (base_manipulator.localEulerAngles.y == angle_out)
                {
                    rotating_angle = angle_inp - angle_out;
                    StartCoroutine(down_manipulator());
                    //StartCoroutine(rotate_manipulator());
                }  
            }
            else if (base_manipulator.localEulerAngles.y == angle_out)
            {
                
            }
        }
    }

    private Transform get_claw()
    {
        return base_manipulator.Find("bone_hand_bottom/bone_hand_top/claw");
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
        if (available_item != null)
            item_grab = available_item.gameObject;
    
        item_grab.GetComponent<Rigidbody>().isKinematic = true;
        item_grab.transform.SetParent(get_claw());
        yield return null;
        StartCoroutine(up_manipulator());
    }

    IEnumerator ungrab_manipulator()
    {
        item_grab.GetComponent<Rigidbody>().isKinematic = false;
        item_grab.transform.SetParent(GameObject.Find("conveyor").transform);
        item_grab = null;
        yield return null;
        StartCoroutine(up_manipulator());
    }

    IEnumerator down_manipulator()
    {
        Transform bone_hand_bottom = base_manipulator.Find("bone_hand_bottom");
        Transform bone_hand_top = bone_hand_bottom.Find("bone_hand_top");

        Quaternion startRotation_bottom = bone_hand_bottom.rotation;
        Quaternion targetRotation_bottom = bone_hand_bottom.rotation * Quaternion.Euler(-50, 0, 0);
        Quaternion startRotation_top = bone_hand_top.rotation;
        Quaternion targetRotation_top = bone_hand_top.rotation * Quaternion.Euler(0, 0, 0);

        float timeElapsed = 0;
        is_grab = true;
        while (timeElapsed < speed)
        {
            bone_hand_bottom.rotation = Quaternion.Slerp(startRotation_bottom, targetRotation_bottom, timeElapsed / speed);
            bone_hand_top.rotation = Quaternion.Slerp(startRotation_top, targetRotation_top, timeElapsed / speed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bone_hand_bottom.rotation = targetRotation_bottom;
        bone_hand_top.rotation = targetRotation_top;

        if (item_grab == null)
            StartCoroutine(grab_manipulator());
        else
            StartCoroutine(ungrab_manipulator());
    }

    IEnumerator up_manipulator()
    {
        Transform bone_hand_bottom = base_manipulator.Find("bone_hand_bottom");
        Transform bone_hand_top = bone_hand_bottom.Find("bone_hand_top");

        Quaternion startRotation_bottom = bone_hand_bottom.rotation;
        Quaternion targetRotation_bottom = bone_hand_bottom.rotation * Quaternion.Euler(50, 0, 0);
        Quaternion startRotation_top = bone_hand_top.rotation;
        Quaternion targetRotation_top = bone_hand_top.rotation * Quaternion.Euler(0, 0, 0);

        float timeElapsed = 0;
        while (timeElapsed < speed)
        {
            bone_hand_bottom.rotation = Quaternion.Slerp(startRotation_bottom, targetRotation_bottom, timeElapsed / speed);
            bone_hand_top.rotation = Quaternion.Slerp(startRotation_top, targetRotation_top, timeElapsed / speed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bone_hand_bottom.rotation = targetRotation_bottom;
        bone_hand_top.rotation = targetRotation_top;
        is_grab = false;
        StartCoroutine(rotate_manipulator());
    }
}
