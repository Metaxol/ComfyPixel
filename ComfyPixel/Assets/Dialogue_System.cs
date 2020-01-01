
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogue_System : MonoBehaviour {

    private Utility utility;

    public void run_dialogue(List<string> lines, Sprite[] sprites, Text TextBox, float scroll_speed, string box_name)
    {
        //just uses utility-class function to start dialogue
        utility.run_text(lines, TextBox, scroll_speed, box_name);
    }

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
    }
}
