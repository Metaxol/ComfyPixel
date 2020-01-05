
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogue_System : MonoBehaviour {

    private Utility utility;

    public void run_dialogue(List<string> lines, Text TextBox)
    {
        //just uses utility-class function to start dialogue
        utility.run_text(lines, TextBox, 0.1f);
    }

    //let_options_choose(new string[] { "choosing_button0", "choosing_button1"});

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
    }
}

