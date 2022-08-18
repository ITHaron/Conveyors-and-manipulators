using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class press_controller : MonoBehaviour
{
    [SerializeField] string tag;
    [SerializeField] Material material;
    [SerializeField] private float speed = 5;
    private bool isMove = false;
    private int direction = 0;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        Rigidbody press = transform.Find("press").GetComponent<Rigidbody>();
        startPos = press.position;
        endPos = startPos - new Vector3(0, 0.28f, 0);

        print(Application.version);
    }

    void OnMouseDown()
    {
        if (!isMove)
        {
            StartCoroutine(Down());
        }
    }

    public void sticker(GameObject obj)
    {
        obj.GetComponent<Renderer>().sharedMaterial = material;
        obj.GetComponent<item>().set_tag(tag);
        direction = 1;
    }

    IEnumerator Down()
    {
        direction = -1;
        isMove = true;
        Rigidbody press = transform.Find("press").GetComponent<Rigidbody>();
        Vector3 pos = press.position;

        while (pos.y > endPos.y && direction < 0)
        {
            pos -= new Vector3(0, 0.01f, 0) * speed * Time.fixedDeltaTime;
            press.MovePosition(pos);
            yield return null;
        }
        press.position = endPos;
        StartCoroutine(Up());
    }

    IEnumerator Up()
    {
        direction = 1;
        Rigidbody press = transform.Find("press").GetComponent<Rigidbody>();
        Vector3 pos = press.position;

        while (pos.y < startPos.y && direction > 0)
        {
            pos += new Vector3(0, 0.01f, 0) * speed * Time.fixedDeltaTime;
            press.MovePosition(pos);
            yield return null;
        }
        press.position = startPos;
        isMove = false;
    }
}
