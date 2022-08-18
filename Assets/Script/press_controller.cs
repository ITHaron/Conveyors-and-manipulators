using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class press_controller : MonoBehaviour
{
    private float speed = 5;
    private int direction = -1;
    private bool isMove = false;

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (!isMove)
        {
            StartCoroutine(Down());
            print("test");
        }
        
    }


    // IEnumerator Move_press()
    // {
    //     Rigidbody press = transform.Find("press").GetComponent<Rigidbody>();
    //     Vector3 startPos = press.position;
    //     Vector3 stopPos = startPos - new Vector3(0, 0.28, 0);
        
    //     if(direction<0)
    //     {
    //         if (press.position < stopPos){
    //             direction *= -1;
    //         }
    //     }
    //     distance = Vector3.back * speed * Time.fixedDeltaTime;

    //     rBody.position += distance * direction;
    //     rBody.MovePosition(pos);

    //     float timeElapsed = 0;
    //     Quaternion startRotation = base_manipulator.rotation;
    //     Quaternion targetRotation = base_manipulator.rotation * Quaternion.Euler(0, rotating_angle, 0);
    //     while (timeElapsed < speed)
    //     {
    //         base_manipulator.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / speed);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     base_manipulator.rotation = targetRotation;
    //     rotating = false;
    // }

    IEnumerator Down()
    {
        Rigidbody press = transform.Find("press").GetComponent<Rigidbody>();
        Vector3 pos = press.position;
        Vector3 stopPos = pos - new Vector3(0, 0.28f, 0);

        while (pos.y > stopPos.y || direction  > 0)
        {
            pos -= new Vector3(0, 0.01f, 0) * speed * Time.fixedDeltaTime;
            press.MovePosition(pos);
            yield return null;
        }
    }
}
