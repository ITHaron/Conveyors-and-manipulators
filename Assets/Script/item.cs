using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private string tag;
    
    public void set_tag(string tag)
    {
        this.tag = tag;
    }

    public string get_tag()
    {
        return this.tag;
    }
}
