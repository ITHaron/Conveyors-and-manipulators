using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class game_controller : MonoBehaviour
{
    [SerializeField] private int cirle_count;
    [SerializeField] private int square_count;
    [SerializeField] private Transform UI;
    // Start is called before the first frame update

    void Start()
    {
        UI = transform.Find($"Canvas/Panel/circle_counter");
    }
    public void update_counter(string tag)
    {
        switch (tag){
            case "circle": 
            {
                cirle_count++; 
                upadate_text("circle_counter", cirle_count);
                break;
                }
            case "square": 
            {
                square_count++;
                upadate_text("square_counter", square_count);
                break;
            }
            default: break;
        }
    }

    private void upadate_text(string name, int value){
        var text = transform.Find($"Canvas/Panel/{name}").GetComponent<TextMeshProUGUI>();
        text.text = value.ToString();
    }
}
